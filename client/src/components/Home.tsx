import React, { useEffect, useState } from "react";
import { http } from "../http.ts";
import { Customer } from '../Api.ts';

export default function Home() {
    const [customers, setCustomers] = useState<Customer[]>([]);

    useEffect(() => {
        http.api.customerGetAllCustomers()
            .then(response => {
                setCustomers(response.data);
            })
            .catch(error => {
                console.error('Error fetching customers:', error);
            });
    }, []);

    return (
        <div className="max-w-full overflow-x-auto p-4">
            <h1 className="text-3xl font-bold mb-6">Customers</h1>
            {customers.length > 0 ? (
                <table className="min-w-full border-collapse border border-gray-300">
                    <thead className="bg-gray-200">
                    <tr>
                        <th className="border border-gray-300 p-2 text-left">Name</th>
                        <th className="border border-gray-300 p-2 text-left">Email</th>
                        <th className="border border-gray-300 p-2 text-left">Phone</th>
                        <th className="border border-gray-300 p-2 text-left">Address</th>
                        <th className="border border-gray-300 p-2 text-left">Orders</th>
                    </tr>
                    </thead>
                    <tbody>
                    {customers.map(customer => (
                        <tr key={customer.id} className="hover:bg-gray-100">
                            <td className="border border-gray-300 p-2">{customer.name}</td>
                            <td className="border border-gray-300 p-2">{customer.email || 'N/A'}</td>
                            <td className="border border-gray-300 p-2">{customer.phone || 'N/A'}</td>
                            <td className="border border-gray-300 p-2">{customer.address || 'N/A'}</td>
                            <td className="border border-gray-300 p-2">
                                {customer.orders && customer.orders.length > 0 ? (
                                    <ul className="list-disc pl-5">
                                        {customer.orders.map(order => (
                                            <li key={order.id}>
                                                Order ID: {order.id} - {order.totalAmount?.toFixed(2)}$
                                            </li>
                                        ))}
                                    </ul>
                                ) : (
                                    <span>No orders</span>
                                )}
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            ) : (
                <p>Loading customers...</p>
            )}
        </div>
    );
}
