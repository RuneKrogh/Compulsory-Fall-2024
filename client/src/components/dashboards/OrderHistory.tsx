import React, { useEffect, useState } from "react";
import { http } from "../main/http.ts"; // Ensure you have this import
import { Order} from "../main/Api.ts";

export default function OrderHistory() {
    const [orders, setOrders] = useState<Order[]>([]);
    const customerId = 1; // Hardcoded customer ID

    useEffect(() => {
        const fetchOrderHistory = async () => {
            try {
                const response = await http.api.orderGetOrdersByCustomerId(customerId); // Make sure this API exists
                setOrders(response.data);
            } catch (error) {
                console.error('Error fetching order history:', error);
            }
        };

        fetchOrderHistory();
    }, []);

    return (
        <div className="max-w-full overflow-x-auto p-1">
            <h1 className="flex flex-col text-2xl font-bold items-center">Order History</h1>
            {orders.length > 0 ? (
                <div className="w-3/5 mx-auto">
                    <table className="table w-full">
                        <thead>
                        <tr>
                            <th className="text-xl px-1 py-1 text-center">ID</th>
                            <th className="text-xl px-1 py-1 text-center">Order Date</th>
                            <th className="text-xl px-1 py-1 text-center">Delivery Date</th>
                            <th className="text-xl px-1 py-1 text-center">Total Amount</th>
                            <th className="text-xl px-1 py-1 text-center">Status</th>
                        </tr>
                        </thead>
                        <tbody>
                        {orders.map(order => (
                            <tr key={order.id}>
                                <td className="px-1 py-1 text-center">{order.id}</td>
                                <td className="px-1 py-1 text-center">{new Date(order.orderDate).toLocaleDateString()}</td>
                                <td className="px-1 py-1 text-center">{new Date(order.deliveryDate).toLocaleDateString()}</td>
                                <td className="px-1 py-1 text-center">{order.totalAmount.toFixed(2)} DKK</td>
                                <td className="px-1 py-1 text-center">{order.status}</td>
                            </tr>
                        ))}
                        </tbody>
                    </table>
                </div>
            ) : (
                <p className="mt-4">No orders found for this customer.</p>
            )}
        </div>
    );
}
