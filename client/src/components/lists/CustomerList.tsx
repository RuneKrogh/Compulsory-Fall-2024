import React, { useEffect, useState } from "react";
import { http } from "../main/http.ts";
import { customersAtom } from '../../atoms/CustomerAtom.tsx';
import { useAtom } from "jotai";
import { customerPageAtom } from "../../atoms/PageAtom.tsx";
import Modal from "../modals/Modal.tsx";

export default function CustomerList() {
    const [customers, setCustomers] = useAtom(customersAtom);
    const [currentPage, setCurrentPage] = useAtom(customerPageAtom);
    const customersPerPage = 15;
    const [searchQuery, setSearchQuery] = useState("");
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [newCustomer, setNewCustomer] = useState({ name: "", email: "", phone: "", address: "" });
    const [editingCustomerId, setEditingCustomerId] = useState(null);

    // Fetch customers if the list is empty
    useEffect(() => {
        if (customers.length === 0) {
            http.api.customerGetAllCustomers()
                .then(response => {
                    setCustomers(response.data);
                })
                .catch(error => {
                    console.error('Error fetching customers:', error);
                });
        }
    }, [customers, setCustomers]);

    // Filter customers based on search query
    const filteredCustomers = customers.filter(customer => {
        const fullName = `${customer.name} ${customer.email} ${customer.phone}`.toLowerCase();
        return fullName.includes(searchQuery.toLowerCase());
    });

    const indexOfLastCustomer = currentPage * customersPerPage;
    const indexOfFirstCustomer = indexOfLastCustomer - customersPerPage;
    const displayedCustomers = filteredCustomers.slice(indexOfFirstCustomer, indexOfLastCustomer);
    const totalPages = Math.ceil(filteredCustomers.length / customersPerPage);

    const handlePageChange = (pageNumber) => {
        setCurrentPage(pageNumber);
    };

    const handleAddCustomer = () => {
        setNewCustomer({ name: "", email: "", phone: "", address: "" });
        setEditingCustomerId(null);
        setIsModalOpen(true);
    };

    const handleEditCustomer = (customer) => {
        setNewCustomer(customer);
        setEditingCustomerId(customer.id);
        setIsModalOpen(true);
    };

    const handleModalSubmit = () => {
        if (editingCustomerId) {
            // Update existing customer
            http.api.customerUpdateCustomer(editingCustomerId, newCustomer)
                .then(response => {
                    const updatedCustomer = response.data; // Ensure API returns updated customer

                    // If API does not return the full customer, manually update it with form data
                    const updatedCustomers = customers.map(customer =>
                        customer.id === editingCustomerId
                            ? { ...customer, ...newCustomer } // Update customer in local state
                            : customer
                    );
                    setCustomers(updatedCustomers); // Update state to trigger re-render
                    setIsModalOpen(false); // Close the modal
                })
                .catch(error => {
                    console.error('Error updating customer:', error);
                });
        } else {
            // Add new customer
            http.api.customerAddCustomer(newCustomer)
                .then(response => {
                    setCustomers([...customers, response.data]); // Add the new customer to the existing list
                    setIsModalOpen(false); // Close the modal
                })
                .catch(error => {
                    console.error('Error adding customer:', error);
                });
        }
        // Reset form
        setNewCustomer({ name: "", email: "", phone: "", address: "" });
    };

    // New handleDeleteCustomer function
    const handleDeleteCustomer = (customerId) => {
        // Confirm delete action
        if (window.confirm("Are you sure you want to delete this customer?")) {
            // API call to delete customer
            http.api.customerDeleteCustomer(customerId)
                .then(() => {
                    // Update customers state by filtering out the deleted customer
                    const updatedCustomers = customers.filter(customer => customer.id !== customerId);
                    setCustomers(updatedCustomers);
                })
                .catch(error => {
                    console.error('Error deleting customer:', error);
                });
        }
    };

    return (
        <div className="max-w-full overflow-x-auto p-1">
            <div className="flex flex-col items-center mb-4">
                <h1 className="text-2xl font-bold">Customers</h1>
                <div className="flex items-center my-4">
                    <input
                        type="text"
                        placeholder="Search..."
                        value={searchQuery}
                        onChange={(e) => {
                            setSearchQuery(e.target.value);
                            setCurrentPage(1); // Reset to first page when searching
                        }}
                        className="input input-bordered input-sm w-full max-w-xs mr-2"
                    />
                    <button onClick={handleAddCustomer} className="btn btn-sm">
                        Add Customer
                    </button>
                </div>
            </div>
            {customers.length > 0 ? (
                <div className="w-3/5 mx-auto">
                    <table className="table w-full">
                        <thead>
                        <tr>
                            <th className="text-xl px-1 py-1 text-center">ID</th>
                            <th className="text-xl px-1 py-1 text-center">Name</th>
                            <th className="text-xl px-1 py-1 text-center">Email</th>
                            <th className="text-xl px-1 py-1 text-center">Phone</th>
                            <th className="text-xl px-1 py-1 text-center">Address</th>
                            <th className="text-xl px-1 py-1 text-center">Orders</th>
                            <th className="text-xl px-1 py-1 text-center">Actions</th>
                        </tr>
                        </thead>
                        <tbody>
                        {displayedCustomers.map(customer => (
                            <tr key={customer.id}>
                                <td className="px-1 py-1 text-center">{customer.id}</td>
                                <td className="px-1 py-1 text-center">{customer.name}</td>
                                <td className="px-1 py-1 text-center">{customer.email || 'N/A'}</td>
                                <td className="px-1 py-1 text-center">{customer.phone || 'N/A'}</td>
                                <td className="px-1 py-1 text-center">{customer.address || 'N/A'}</td>
                                <td className="px-1 py-1 text-center">0</td>
                                <td className="px-1 py-1 text-center">
                                    <div className="flex justify-center space-x-1">
                                        <button
                                            className="btn btn-sm"
                                            onClick={() => handleEditCustomer(customer)}
                                        >
                                            Edit
                                        </button>
                                        <button
                                            className="btn btn-sm btn-danger"
                                            onClick={() => handleDeleteCustomer(customer.id)}
                                        >
                                            Delete
                                        </button>
                                    </div>
                                </td>
                            </tr>
                        ))}
                        </tbody>
                    </table>

                    <div className="flex mt-4 justify-center">
                        {Array.from({ length: totalPages }, (_, index) => (
                            <button
                                key={index + 1}
                                onClick={() => handlePageChange(index + 1)}
                                className={`mx-1 px-2 py-1 my-1 border rounded ${
                                    currentPage === index + 1 ? 'bg-gray-700 text-white' : null
                                }`}
                            >
                                {index + 1}
                            </button>
                        ))}
                    </div>
                </div>
            ) : (
                <p>Loading customers...</p>
            )}

            {/* Add/Edit Customer Modal */}
            <Modal
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                onSubmit={handleModalSubmit}
                title={editingCustomerId ? "Edit Customer" : "Add New Customer"}
            >
                <div>
                    <label className="block mb-2">Name</label>
                    <input
                        type="text"
                        value={newCustomer.name}
                        onChange={(e) => setNewCustomer({ ...newCustomer, name: e.target.value })}
                        className="input input-bordered w-full mb-4"
                        required
                    />
                </div>
                <div>
                    <label className="block mb-2">Email</label>
                    <input
                        type="email"
                        value={newCustomer.email}
                        onChange={(e) => setNewCustomer({ ...newCustomer, email: e.target.value })}
                        className="input input-bordered w-full mb-4"
                        required
                    />
                </div>
                <div>
                    <label className="block mb-2">Phone</label>
                    <input
                        type="tel"
                        value={newCustomer.phone}
                        onChange={(e) => setNewCustomer({ ...newCustomer, phone: e.target.value })}
                        className="input input-bordered w-full mb-4"
                        required
                    />
                </div>
                <div>
                    <label className="block mb-2">Address</label>
                    <input
                        type="text"
                        value={newCustomer.address}
                        onChange={(e) => setNewCustomer({ ...newCustomer, address: e.target.value })}
                        className="input input-bordered w-full mb-4"
                        required
                    />
                </div>
            </Modal>
        </div>
    );
}
