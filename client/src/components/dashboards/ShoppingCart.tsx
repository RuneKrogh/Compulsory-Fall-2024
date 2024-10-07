import React from "react";
import { shoppingCartAtom } from "../../atoms/ShoppingCartAtom";
import { useAtom } from "jotai";
import { http } from "../main/http"; // Adjust the import path according to your project structure

export default function ShoppingCart() {
    const [cartItems, setCartItems] = useAtom<[]>(shoppingCartAtom); // Specify the type here

    // Remove an item from the cart
    const handleRemoveFromCart = (id) => {
        // @ts-ignore
        setCartItems((prevItems) => prevItems.filter(item => item.id !== id));
        console.log(`Removed item with ID ${id} from cart.`);
    };

    const handleCheckout = async () => {
        // Calculate total amount based on cart items
        const totalAmount = cartItems.reduce((total, item) => {
            // @ts-ignore
            return total + (item.price * item.quantity);
        }, 0);

        // Create orderDate and deliveryDate
        const orderDate = new Date();
        const deliveryDate = new Date(orderDate);
        deliveryDate.setDate(orderDate.getDate() + 3);

        // Format deliveryDate to match DateOnly in backend (YYYY-MM-DD format)
        const formattedDeliveryDate = deliveryDate.toISOString().split('T')[0];

        const order = {
            orderDate: orderDate.toISOString(), // Full date and time for order date
            deliveryDate: formattedDeliveryDate, // Date only for delivery date
            status: "pending", // Status
            totalAmount: totalAmount, // Total amount
            customerId: 1, // Provide valid customer ID or null if applicable
            orderEntries: cartItems.map(item => ({
                quantity: item.quantity,
                productId: item.id, // Ensure you're passing the correct field for product ID
            })),
        };

        try {
            // Clear the cart after placing the order
            setCartItems([]);
            // Send the order to the backend
            const response = await http.api.orderAddOrder(order);
            console.log("Order placed successfully:", response.data);

        } catch (error) {
            if (error.response) {
                console.error("Error response from server:", error.response.data);
            } else {
                console.error("Error placing order:", error);
            }
        }
    };

    // Calculate total price of items in the cart
    const getTotalPrice = () => {
        // Check if cartItems is empty or not
        if (cartItems.length === 0) return "0.00";
        return cartItems.reduce((total, item) => {
            console.log(item); // Debug: Check the current item
            // @ts-ignore
            return total + (item.price * item.quantity);
        }, 0).toFixed(2);
    };

    return (
        <div className="max-w-full p-4">
            <h1 className="text-2xl font-bold mb-4">Shopping Cart</h1>
            {cartItems.length > 0 ? (
                <div>
                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
                        {cartItems.map(item => (
                            <div key={item.id} className="card shadow-lg border rounded-md p-4">
                                <h2 className="text-lg font-semibold">{item.name}</h2>
                                <p className="text-gray-700">${item.price.toFixed(2)} x {item.quantity}</p>
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
                        <h2 className="text-xl font-semibold">Total: ${getTotalPrice()}</h2>
                        <button
                            onClick={handleCheckout}
                            className="btn btn-primary mt-2"
                        >
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
