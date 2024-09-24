import React, { useEffect, useState } from "react";
import { http } from "../../http.ts";
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

    return (
        <div className="max-w-full overflow-x-auto p-1">
            <div className="flex items-center">
                <h1 className="text-3xl font-bold pr-4">List of Customers</h1>
                <input
                    type="text"
                    placeholder="Search..."
                    value={searchQuery}
                    onChange={(e) => {
                        setSearchQuery(e.target.value);
                        setCurrentPage(1);
                    }}
                    className="input input-bordered input-sm w-full max-w-xs"
                />
            </div>
            {customers.length > 0 ? (
                <div>
                    <table className="table">
                        <thead>
                        <tr>
                            <th className="text-xl px-2 py-1.5">ID</th>
                            <th className="text-xl px-2 py-1">Name</th>
                            <th className="text-xl px-2 py-1">Email</th>
                            <th className="text-xl px-2 py-1">Phone</th>
                            <th className="text-xl px-2 py-1">Address</th>
                            <th className="text-xl px-2 py-1">Orders</th>
                        </tr>
                        </thead>
                        <tbody>
                        {displayedCustomers.map(customer => (
                            <tr key={customer.id}>
                                <td className="px-2 py-1.5">{customer.id}</td>
                                <td className="px-2 py-1">{customer.name}</td>
                                <td className="px-2 py-1">{customer.email || 'N/A'}</td>
                                <td className="px-2 py-1">{customer.phone || 'N/A'}</td>
                                <td className="px-2 py-1">{customer.address || 'N/A'}</td>
                            </tr>
                        ))}
                        </tbody>
                    </table>

                    <div className="flex mt-4">
                        {Array.from({ length: totalPages }, (_, index) => (
                            <button
                                key={index + 1}
                                onClick={() => handlePageChange(index + 1)}
                                className={`mx-1 px-2.5 py-1 my-1 -mt-2 border rounded ${
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
