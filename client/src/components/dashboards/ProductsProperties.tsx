import React from "react";
import ProductsList from "../lists/PapersList.tsx";
import PropertiesList from "../lists/PropertiesList.tsx";

export default function ProductsProperties() {
    return (
        <>
            <div className="flex space-x-4">
                <div className="flex-1">
                    <ProductsList />
                </div>
                <div className="flex-1">
                    <PropertiesList />
                </div>
            </div>
        </>
    );
}
