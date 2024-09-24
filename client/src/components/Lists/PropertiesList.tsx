import React, { useEffect, useState } from "react";
import { http } from "../../http.ts";
import { useAtom } from "jotai";
import { propertiesAtom } from "../../atoms/PropertiesAtom.tsx";

export default function PropertiesList() {
    const [properties, setProperties] = useAtom(propertiesAtom);
    const [searchQuery, setSearchQuery] = useState("");
    const [currentPage, setCurrentPage] = useState(1);
    const propertiesPerPage = 15;

    useEffect(() => {
        // Only fetch properties if the atom is empty
        if (properties.length === 0) {
            http.api.propertyGetAllProperties()
                .then(response => {
                    setProperties(response.data);
                })
                .catch(error => {
                    console.error('Error fetching properties:', error);
                });
        }
    }, [properties, setProperties]);

    // Filter properties based on search query
    const filteredProperties = properties.filter(property => {
        const fullName = `${property.propertyName} ${property.id}`.toLowerCase(); // Adjust this based on available fields
        return fullName.includes(searchQuery.toLowerCase());
    });

    // Calculate the indices for slicing the filtered properties array
    const indexOfLastProperty = currentPage * propertiesPerPage;
    const indexOfFirstProperty = indexOfLastProperty - propertiesPerPage;
    const displayedProperties = filteredProperties.slice(indexOfFirstProperty, indexOfLastProperty);

    // Calculate total pages
    const totalPages = Math.ceil(filteredProperties.length / propertiesPerPage);

    // Handle page change
    const handlePageChange = (pageNumber) => {
        setCurrentPage(pageNumber);
    };

    return (
        <div className="max-w-full overflow-x-auto p-1">
            <div className="flex items-center">
                <h1 className="text-3xl font-bold pr-4">List of Properties</h1>
                <input
                    type="text"
                    placeholder="Search..."
                    value={searchQuery}
                    onChange={(e) => {
                        setSearchQuery(e.target.value);
                        setCurrentPage(1); // Reset to page 1 whenever the search query changes
                    }}
                    className="input input-bordered input-sm w-full max-w-xs"
                />
            </div>

            {properties.length > 0 ? (
                <>
                    <table className="table">
                        <thead>
                        <tr>
                            <th className="text-xl px-2 py-1.5">ID</th>
                            <th className="text-xl px-2 py-1">Name</th>
                        </tr>
                        </thead>
                        <tbody>
                        {displayedProperties.map(property => (
                            <tr key={property.id}>
                                <td className="px-2 py-1.5">{property.id}</td>
                                <td className="px-2 py-1">{property.propertyName}</td>
                            </tr>
                        ))}
                        </tbody>
                    </table>

                    {/* Pagination Controls */}
                    <div className="flex mt-4">
                        {Array.from({ length: totalPages }, (_, index) => (
                            <button
                                key={index + 1}
                                onClick={() => handlePageChange(index + 1)}
                                className={`mx-1 px-2.5 py-1 my-1 border rounded ${
                                    currentPage === index + 1 ? 'bg-gray-700 text-white' : null
                                }`}
                            >
                                {index + 1}
                            </button>
                        ))}
                    </div>
                </>
            ) : (
                <p>Loading properties...</p>
            )}
        </div>
    );
}
