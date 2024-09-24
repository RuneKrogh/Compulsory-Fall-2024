import React, { useEffect, useState } from "react";
import { http } from "../main/http.ts";
import { customersAtom } from '../../atoms/CustomerAtom.tsx';
import { useAtom } from "jotai";
import { customerPageAtom } from "../../atoms/PageAtom.tsx";

export default function CustomerList() {
    const [customers, setCustomers] = useAtom(customersAtom);
    const [currentPage, setCurrentPage] = useAtom(customerPageAtom);
    const customersPerPage = 15;
    const [searchQuery, setSearchQuery] = useState("");

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
        console.log("Add Customer button clicked");
    };

    return (
        <div className="max-w-full overflow-x-auto p-1">
            <div className="flex flex-col items-center mb-4">
                <h1 className="text-2xl font-bold">Customers</h1>
                <div className="flex items-center my-4"> {}
                    <input
                        type="text"
                        placeholder="Search..."
                        value={searchQuery}
                        onChange={(e) => {
                            setSearchQuery(e.target.value);
                            setCurrentPage(1);
                        }}
                        className="input input-bordered input-sm w-full max-w-xs mr-2"
                    />
                    <button
                        onClick={handleAddCustomer}
                        className="btn btn-sm"
                    >
                        Add Customer
                    </button>
                </div>
            </div>
            {customers.length > 0 ? (
                <div className="w-3/5 mx-auto"> {}
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
                                        <button className="btn btn-sm">Edit</button>
                                        <button className="btn btn-sm">Delete</button>
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
        </div>
    );
}
