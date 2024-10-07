import React from "react";
import { shoppingCartAtom } from "../../atoms/ShoppingCartAtom";
import { useAtom } from "jotai";
import { http } from "../main/http"; // Adjust the import path according to your project structure
import toast from 'react-hot-toast'; // Import toast from react-hot-toast

export default function ShoppingCart() {
    const [cartItems, setCartItems] = useAtom(shoppingCartAtom); // Specify the type here

    // Remove an item from the cart
    const handleRemoveFromCart = (id: number) => {
        setCartItems((prevItems) => {
            const updatedItems = prevItems.filter(item => item.id !== id);
            toast.success(`Removed item with ID ${id} from cart.`); // Show toast notification
            return updatedItems;
        });
        console.log(`Removed item with ID ${id} from cart.`);
    };

    const handleCheckout = async () => {
        // Calculate total amount based on cart items
        const totalAmount = cartItems.reduce((total, item) => {
            return total + (item.price * item.quantity);
        }, 0);

        const orderDate = new Date();
        const deliveryDate = new Date(orderDate);
        deliveryDate.setDate(orderDate.getDate() + 3);

        const formattedDeliveryDate = deliveryDate.toISOString().split('T')[0];

        const order = {
            orderDate: orderDate.toISOString(),
            deliveryDate: formattedDeliveryDate,
            status: "pending",
            totalAmount: totalAmount,
            customerId: 1, // Adjust as needed
            orderEntries: cartItems.map(item => ({
                quantity: item.quantity,
                productId: item.id,
            })),
        };

        try {
            // Place the order
            const response = await http.api.orderAddOrder(order);
            console.log("Order placed successfully:", response.data);
            toast.success("Order placed successfully!");

            // Update stock for each item in the cart
            await Promise.all(cartItems.map(async item => {
                await http.api.paperUpdateStock({
                    id: item.id,
                    stock: item.quantity
                });
            }));
            console.log("Stock updated successfully.");

            // Optionally: update stock in the cartItems state after successful update
            setCartItems(cartItems.map(item => ({
                ...item,
                stock: item.stock - item.quantity // Update stock locally
            })));

            // Clear the cart
            setCartItems([]);

        } catch (error) {
            toast.error("Error placing order. Please try again.");
            if (error.response) {
                console.error("Error response from server:", error.response.data);
            } else {
                console.error("Error placing order:", error);
            }
        }
    };


    // Calculate total price of items in the cart
    const getTotalPrice = () => {
        if (cartItems.length === 0) return "0.00";
        return cartItems.reduce((total, item) => {
            return total + (item.price * item.quantity);
        }, 0).toFixed(2);
    };

    return (
        <div className="max-w-full p-4">
            <h1 className="text-2xl font-bold mb-4">Shopping Cart</h1>
            {cartItems.length > 0 ? (
                <div>
                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-4">
                        {cartItems.map(item => (
                            <div key={item.id} className="card shadow-lg border rounded-md p-4">
                                <h2 className="text-lg font-semibold">{item.name}</h2>
                                <p className="text-gray-300">${item.price.toFixed(2)} x {item.quantity}</p>
                                <button
                                    onClick={() => handleRemoveFromCart(item.id)}
                                    className="btn btn-danger mt-2"
                                >
                                    Remove
                                </button>
                            </div>
                        ))}
                    </div>
                    <div className="mt-4">
                        <h2 className="text-xl font-semibold">Total: {getTotalPrice()}$</h2>
                        <button
                            onClick={handleCheckout}
                            className="btn mt-2">
                            Checkout
                        </button>
                    </div>
                </div>
            ) : (
                <p>Your cart is empty.</p>
            )}
        </div>
    );
}
