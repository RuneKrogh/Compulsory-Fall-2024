import React, { useEffect, useState } from "react";
import { http } from "../main/http.ts";
import { useAtom } from "jotai";
import { ordersAtom } from "../../atoms/OrderAtom.tsx";
import { customersAtom } from "../../atoms/CustomerAtom.tsx";
import { orderPageAtom } from "../../atoms/PageAtom.tsx";
import Modal from "../modals/Modal.tsx";
import toast from 'react-hot-toast'; // Import react-hot-toast

export default function OrderList() {
    const [orders, setOrders] = useAtom(ordersAtom);
    const [customers, setCustomers] = useAtom(customersAtom);
    const [currentPage, setCurrentPage] = useAtom(orderPageAtom);
    const [searchQuery, setSearchQuery] = useState("");
    const ordersPerPage = 15;

    // Modal-related states
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [editingOrderId, setEditingOrderId] = useState(null);
    const [selectedStatus, setSelectedStatus] = useState("");

    useEffect(() => {
        if (customers.length === 0) {
            http.api.customerGetAllCustomers()
                .then(response => {
                    setCustomers(response.data);
                })
                .catch(error => {
                    console.error('Error fetching customers:', error);
                    toast.error('Failed to load customers'); // Toast for error loading customers
                });
        }

        if (orders.length === 0) {
            http.api.orderGetAllOrders()
                .then(response => {
                    setOrders(response.data);
                })
                .catch(error => {
                    console.error('Error fetching orders:', error);
                    toast.error('Failed to load orders'); // Toast for error loading orders
                });
        }

    }, [setOrders]);

    const filteredOrders = orders.filter(order => {
        return order.id.toString().includes(searchQuery);
    });

    const indexOfLastOrder = currentPage * ordersPerPage;
    const indexOfFirstOrder = indexOfLastOrder - ordersPerPage;
    const displayedOrders = filteredOrders.slice(indexOfFirstOrder, indexOfLastOrder);

    const totalPages = Math.ceil(filteredOrders.length / ordersPerPage);

    const handlePageChange = (pageNumber) => {
        setCurrentPage(pageNumber);
    };

    const getCustomerNameById = (customerId) => {
        const customer = customers.find(c => c.id === customerId);
        return customer ? customer.name : 'Unknown Customer';
    };

    const formatOrderDate = (dateString) => {
        const date = new Date(dateString);
        return `${date.getUTCDate().toString().padStart(2, '0')}-${(date.getUTCMonth() + 1).toString().padStart(2, '0')}-${date.getUTCFullYear()} ${date.getUTCHours().toString().padStart(2, '0')}:${date.getUTCMinutes().toString().padStart(2, '0')}:${date.getUTCSeconds().toString().padStart(2, '0')}`;
    };

    const formatDeliveryDate = (dateString) => {
        const date = new Date(dateString);
        return `${date.getUTCDate().toString().padStart(2, '0')}-${(date.getUTCMonth() + 1).toString().padStart(2, '0')}-${date.getUTCFullYear()}`;
    };

    // Handle opening the modal and setting the current order to edit
    const handleEditStatus = (order) => {
        setEditingOrderId(order.id);
        setSelectedStatus(order.status); // Pre-fill with current status
        setIsModalOpen(true);
    };

    // Handle submitting the updated status
    const handleModalSubmit = () => {
        const updatedOrder = {
            ...orders.find(order => order.id === editingOrderId),
            status: selectedStatus,
        };

        http.api.orderUpdateOrder(editingOrderId, updatedOrder)
            .then(response => {
                const updatedOrders = orders.map(order =>
                    order.id === editingOrderId ? updatedOrder : order
                );
                setOrders(updatedOrders); // Update the orders state with the modified order
                setIsModalOpen(false); // Close the modal
                toast.success('Order status updated successfully'); // Toast for successful update
            })
            .catch(error => {
                console.error('Error updating order status:', error);
                toast.error('Failed to update order status'); // Toast for error updating order
            });
    };

    return (
        <div className="max-w-full overflow-x-auto p-1">
            <div className="flex flex-col items-center mb-4">
                <h1 className="text-2xl font-bold">Orders</h1>
                <input
                    type="text"
                    placeholder="Search by order ID..."
                    value={searchQuery}
                    onChange={(e) => {
                        setSearchQuery(e.target.value);
                        setCurrentPage(1);
                    }}
                    className="input input-bordered input-sm w-full max-w-xs mt-2"
                />
            </div>
            {orders.length > 0 ? (
                <div className="w-3/5 mx-auto">
                    <table className="table w-full">
                        <thead>
                        <tr>
                            <th className="text-xl px-1 py-1 text-center">ID</th>
                            <th className="text-xl px-1 py-1 text-center">Customer</th>
                            <th className="text-xl px-1 py-1 text-center">Order Date</th>
                            <th className="text-xl px-1 py-1 text-center">Delivery Date</th>
                            <th className="text-xl px-1 py-1 text-center">Total Amount</th>
                            <th className="text-xl px-1 py-1 text-center">Status</th>
                            <th className="text-xl px-1 py-1 text-center">Actions</th>
                        </tr>
                        </thead>
                        <tbody>
                        {displayedOrders.map(order => (
                            <tr key={order.id}>
                                <td className="px-1 py-1 text-center">{order.id}</td>
                                <td className="px-1 py-1 text-center">{getCustomerNameById(order.customerId)}</td>
                                <td className="px-1 py-1 text-center">{order.orderDate ? formatOrderDate(order.orderDate) : 'N/A'}</td>
                                <td className="px-1 py-1 text-center">{order.deliveryDate ? formatDeliveryDate(order.deliveryDate) : 'N/A'}</td>
                                <td className="px-1 py-1 text-center">{order.totalAmount ? `${order.totalAmount.toFixed(2)}$` : 'N/A'}</td>
                                <td className="px-1 py-1 text-center">{order.status}</td>
                                <td className="px-1 py-1 text-center">
                                    <div className="flex justify-center space-x-1">
                                        <button className="btn btn-sm" onClick={() => handleEditStatus(order)}>
                                            Edit Status
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
                                className={`mx-1 px-2 py-1 border rounded ${currentPage === index + 1 ? 'bg-gray-700 text-white' : ''}`}
                            >
                                {index + 1}
                            </button>
                        ))}
                    </div>
                </div>
            ) : (
                <p>Loading orders...</p>
            )}

            {/* Modal for editing order status */}
            <Modal
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                onSubmit={handleModalSubmit}
                title="Edit Order Status"
            >
                <div>
                    <label className="block mb-2">Order Status</label>
                    <select
                        value={selectedStatus}
                        onChange={(e) => setSelectedStatus(e.target.value)}
                        className="select select-bordered w-full mb-4"
                    >
                        <option value="pending">Pending</option>
                        <option value="completed">Completed</option>
                        <option value="cancelled">Cancelled</option>
                    </select>
                </div>
            </Modal>
        </div>
    );
}
