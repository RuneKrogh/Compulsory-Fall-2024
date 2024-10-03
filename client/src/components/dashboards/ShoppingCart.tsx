import React from "react";
import { http } from "../main/http"; // Adjust the import path according to your project structure
import { shoppingCartAtom} from "../../atoms/ShoppingCartAtom.tsx";
import { useAtom } from "jotai";

export default function ShoppingCart() {
    const [cartItems, setCartItems] = useAtom(shoppingCartAtom);

    const handleRemoveFromCart = (id) => {
        console.log("Removed item from cart.")
    };

    const handleCheckout = () => {
        console.log("You have ordered items: ", cartItems);
    };

    const getTotalPrice = () => {
        return null
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
};
