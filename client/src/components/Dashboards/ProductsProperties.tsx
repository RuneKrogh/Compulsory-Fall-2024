import React from "react";
import ProductsList from "../Lists/PapersList.tsx"; // Adjust the import path as needed
import PropertiesList from "../Lists/PropertiesList.tsx"; // Adjust the import path as needed

export default function ProductsProperties() {
    return (
        <>
            <div className="flex space-x-4"> {/* Flex container for side-by-side layout */}
                <div className="flex-1"> {/* Allow ProductsList to take available space */}
                    <ProductsList />
                </div>
                <div className="flex-1"> {/* Allow PropertiesList to take available space */}
                    <PropertiesList />
                </div>
            </div>
        </>
    );
}
