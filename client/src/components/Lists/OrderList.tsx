import React, { useEffect, useState } from "react";
import { http } from "../../http.ts";
import { useAtom } from "jotai";
import { ordersAtom } from "../../atoms/OrderAtom.tsx";
import { customersAtom } from "../../atoms/CustomerAtom.tsx"; // Import the atom
import { orderPageAtom } from "../../atoms/PageAtom.tsx"; // Import the page atom

export default function OrderList() {
    const [orders, setOrders] = useAtom(ordersAtom); // Use the atom
    const [customers, setCustomers] = useAtom(customersAtom);
    const [currentPage, setCurrentPage] = useAtom(orderPageAtom); // Use the page atom
    const [searchQuery, setSearchQuery] = useState(""); // State for search query
    const ordersPerPage = 15; // Number of orders per page

    useEffect(() => {
        // Fetch orders
        http.api.orderGetAllOrders()
            .then(response => {
                setOrders(response.data);
            })
            .catch(error => {
                console.error('Error fetching orders:', error);
            });
    }, [customers, setCustomers, setOrders]);

    // Filter orders based on search query (by ID)
    const filteredOrders = orders.filter(order => {
        // @ts-ignore
        return order.id.toString().includes(searchQuery); // Filter by order ID
    });

    // Calculate the indices for slicing the filtered orders array
    const indexOfLastOrder = currentPage * ordersPerPage;
    const indexOfFirstOrder = indexOfLastOrder - ordersPerPage;
    const displayedOrders = filteredOrders.slice(indexOfFirstOrder, indexOfLastOrder);

    // Calculate total pages
    const totalPages = Math.ceil(filteredOrders.length / ordersPerPage);

    // Handle page change
    const handlePageChange = (pageNumber) => {
        setCurrentPage(pageNumber);
    };

    // Function to get customer name by ID
    const getCustomerNameById = (customerId) => {
        const customer = customers.find(c => c.id === customerId);
        return customer ? customer.name : 'Unknown Customer';
    };

    // Helper function to format order date and time
    const formatOrderDate = (dateString) => {
        const date = new Date(dateString);
        const day = String(date.getUTCDate()).padStart(2, '0');
        const month = String(date.getUTCMonth() + 1).padStart(2, '0'); // Months are zero-indexed
        const year = date.getUTCFullYear();
        const hours = String(date.getUTCHours()).padStart(2, '0');
        const minutes = String(date.getUTCMinutes()).padStart(2, '0');
        const seconds = String(date.getUTCSeconds()).padStart(2, '0');

        return `${day}-${month}-${year} ${hours}:${minutes}:${seconds}`;
    };

    // Helper function to format delivery date
    const formatDeliveryDate = (dateString) => {
        const date = new Date(dateString);
        const day = String(date.getUTCDate()).padStart(2, '0');
        const month = String(date.getUTCMonth() + 1).padStart(2, '0'); // Months are zero-indexed
        const year = date.getUTCFullYear();

        return `${day}-${month}-${year}`;
    };

    return (
        <div className="max-w-full overflow-x-auto p-1">
            <div className="flex items-center">
                <h1 className="text-3xl font-bold pr-4">List of Orders</h1>
                    <input
                        type="text"
                        placeholder="Search by order ID..."
                        value={searchQuery}
                        onChange={(e) => {
                            setSearchQuery(e.target.value);
                            setCurrentPage(1); // Reset to page 1 whenever the search query changes
                        }}
                        className="input input-bordered input-sm w-full max-w-xs"
                    />
                </div>
                {orders.length > 0 ? (
                    <div>
                        <table className="table">
                            <thead>
                            <tr>
                                <th className="text-xl px-2 py-1.5">ID</th>
                                <th className="text-xl px-2 py-1">Customer</th>
                                <th className="text-xl px-2 py-1">Order Date</th>
                                <th className="text-xl px-2 py-1">Delivery Date</th>
                                <th className="text-xl px-2 py-1">Total Amount</th>
                                <th className="text-xl px-2 py-1">Status</th>
                            </tr>
                            </thead>
                            <tbody>
                            {displayedOrders.map(order => (
                                <tr key={order.id}>
                                    <td className="px-2 py-1.5">{order.id}</td>
                                    <td className="px-2 py-1">{getCustomerNameById(order.customerId)}</td>
                                    <td className="px-2 py-1">{order.orderDate ? formatOrderDate(order.orderDate) : 'N/A'}</td>
                                    <td className="px-2 py-1">{order.deliveryDate ? formatDeliveryDate(order.deliveryDate) : 'N/A'}</td>
                                    <td className="px-2 py-1">{order.totalAmount ? `${order.totalAmount.toFixed(2)} DKK` : 'N/A'}</td>
                                    <td className="px-2 py-1">{order.status}</td>
                                </tr>
                            ))}
                            </tbody>
                        </table>

                        {/* Pagination Controls */}
                        <div className="flex mt-4">
                            {Array.from({length: totalPages}, (_, index) => (
                                <button
                                    key={index + 1}
                                    onClick={() => handlePageChange(index + 1)}
                                    className={`mx-1 px-2.5 py-1 mb-4 -mt-2 border rounded ${
                                        currentPage === index + 1 ? 'bg-gray-700 text-white' : null
                                    }`}
                                >
                                    {index + 1}
                                </button>
                            ))}
                        </div>
                    </div>
                ) : (
                    <p>Loading orders...</p>
                )}
            </div>
            );
}
