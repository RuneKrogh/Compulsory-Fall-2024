import React, { useEffect, useState } from "react";
import { http } from "../main/http"; // Adjust the import path according to your project structure
import { papersAtom } from "../../atoms/PapersAtom"; // Atom for products
import { shoppingCartAtom } from "../../atoms/ShoppingCartAtom"; // Atom for shopping cart
import { useAtom } from "jotai";
import toast from 'react-hot-toast'; // Import toast from react-hot-toast

export default function Home() {
    const [papers, setPapers] = useAtom(papersAtom);
    const [searchQuery, setSearchQuery] = useState("");
    const [quantities, setQuantities] = useState<{ [key: number]: number }>({});
    const [cartItems, setCartItems] = useAtom(shoppingCartAtom); // Use shopping cart atom
    const [sortOption, setSortOption] = useState("priceAsc"); // Default sort by price ascending

    useEffect(() => {
        if (papers.length === 0) {
            http.api.paperGetAllPapers()
                .then(response => {
                    setPapers(response.data);
                })
                .catch(error => {
                    console.error('Error fetching products:', error);
                });
        }
    }, [papers, setPapers]);

    // Filter products based on the search query and sort them
    const filteredProducts = papers.filter(paper =>
        paper.name.toLowerCase().includes(searchQuery.toLowerCase())
    ).sort((a, b) => {
        switch (sortOption) {
            case 'priceAsc':
                return a.price - b.price; // Sort by price ascending
            case 'priceDesc':
                return b.price - a.price; // Sort by price descending
            case 'nameAsc':
                return a.name.localeCompare(b.name); // Sort by name A-Z
            case 'nameDesc':
                return b.name.localeCompare(a.name); // Sort by name Z-A
            default:
                return 0;
        }
    });

    const handleAddToCart = (id: number | undefined): void => {
        const quantity = quantities[id] || 1;

        if (quantity > 0) {
            // Check if the item is already in the cart
            const existingItem = cartItems.find(item => item.id === id);
            if (existingItem) {
                // Update quantity if item already exists
                setCartItems(prev => prev.map(item =>
                    item.id === id ? { ...item, quantity: item.quantity + quantity } : item
                ));
                toast.success(`Updated quantity of paper ID ${id} in cart.`);
            } else {
                // Add new item to the cart
                const paperToAdd = papers.find(paper => paper.id === id);
                if (paperToAdd) {
                    const newItem = { ...paperToAdd, quantity }; // Create a new order entry item
                    setCartItems(prev => [...prev, newItem]); // Add new item to the cart
                    toast.success(`Added ${quantity} of paper "${paperToAdd.name}" to the cart.`);
                }
            }

            // Reset quantity for the item in the input
            setQuantities(prev => ({ ...prev, [id]: 1 }));
            console.log(`Adding ${quantity} of paper ID ${id} to cart.`);
        } else {
            toast.error("Please enter a quantity greater than 0.");
        }
    };

    const handleQuantityChange = (id: number, value: number) => {
        setQuantities(prev => ({ ...prev, [id]: value }));
    };

    return (
        <div className="max-w-full overflow-x-auto p-4">
            <div className="flex flex-col items-center mb-4">
                <h1 className="text-2xl font-bold">Store Products</h1>

                {/* Search Input */}
                <input
                    type="text"
                    placeholder="Search for products..."
                    value={searchQuery}
                    onChange={(e) => setSearchQuery(e.target.value)}
                    className="input input-bordered input-sm w-full max-w-md my-4"
                />

                {/* Sort Options */}
                <div className="mb-4">
                    <select
                        value={sortOption}
                        onChange={(e) => setSortOption(e.target.value)}
                        className="select select-bordered w-full max-w-md"
                    >
                        <option value="priceAsc">Sort by Price (Low to High)</option>
                        <option value="priceDesc">Sort by Price (High to Low)</option>
                        <option value="nameAsc">Sort by Name (A-Z)</option>
                        <option value="nameDesc">Sort by Name (Z-A)</option>
                    </select>
                </div>
            </div>

            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
                {filteredProducts.length > 0 ? (
                    filteredProducts.map(paper => (
                        <div key={paper.id} className="card shadow-lg border rounded-md p-4">
                            <h2 className="text-lg font-semibold">{paper.name}</h2>
                            <p className="text-gray-300">{paper.price.toFixed(2)} DKK</p>
                            <p className="text-gray-500">{paper.properties}</p>
                            <p className="text-gray-400">Stock: {paper.stock}</p> {/* Display stock info here */}

                            {/* Quantity Input */}
                            <div className="flex items-center mt-2">
                                <button
                                    onClick={() => handleAddToCart(paper.id)}
                                    className="btn btn-neutral mr-2"
                                    disabled={paper.stock <= 0} // Disable button if out of stock
                                >
                                    Add to Cart
                                </button>
                                <input
                                    type="number"
                                    min="1"
                                    value={quantities[paper.id] || 1}
                                    onChange={(e) => handleQuantityChange(paper.id, parseInt(e.target.value))}
                                    className="input input-bordered w-16 mr-2"
                                />
                            </div>
                        </div>
                    ))
                ) : (
                    <p>No papers found.</p>
                )}
            </div>
        </div>
    );
}
