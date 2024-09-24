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

    const filteredProperties = properties.filter(property => {
        const fullName = `${property.propertyName} ${property.id}`.toLowerCase();
        return fullName.includes(searchQuery.toLowerCase());
    });

    const indexOfLastProperty = currentPage * propertiesPerPage;
    const indexOfFirstProperty = indexOfLastProperty - propertiesPerPage;
    const displayedProperties = filteredProperties.slice(indexOfFirstProperty, indexOfLastProperty);

    const totalPages = Math.ceil(filteredProperties.length / propertiesPerPage);

    const handlePageChange = (pageNumber) => {
        setCurrentPage(pageNumber);
    };

    function handleAddProperty() {
        console.log("Add property");
    }

    return (
        <div className="max-w-full overflow-x-auto p-1">
            <div className="flex flex-col items-center mb-4">
                <h1 className="text-2xl font-bold">Properties</h1>
                <div className="flex items-center my-4">
                    <input
                        type="text"
                        placeholder="Search..."
                        value={searchQuery}
                        onChange={(e) => {
                            setSearchQuery(e.target.value);
                            setCurrentPage(1);                         }}
                        className="input input-bordered input-sm w-full max-w-xs mr-2"
                    />
                    <button
                        onClick={handleAddProperty}
                        className="btn btn-sm"
                    >
                        Add Property
                    </button>
                </div>
            </div>

            {properties.length > 0 ? (
                <div className="w-3/5 mx-auto">
                    <table className="table w-full">
                        <thead>
                        <tr>
                            <th className="text-xl px-1 py-1 text-center">ID</th>
                            <th className="text-xl px-1 py-1 text-center">Name</th>
                            <th className="text-xl px-1 py-1 text-center">Actions</th>
                        </tr>
                        </thead>
                        <tbody>
                        {displayedProperties.map(property => (
                            <tr key={property.id}>
                                <td className="px-1 py-1 text-center">{property.id}</td>
                                <td className="px-1 py-1 text-center">{property.propertyName}</td>
                                <td className="px-1 py-1 text-center">
                                    <div className="flex justify-center space-x-1">
                                        <button className="btn btn-sm">
                                            Edit
                                        </button>
                                        <button className="btn btn-sm">
                                            Delete
                                        </button>
                                    </div>
                                </td>
                            </tr>
                        ))}
                        </tbody>
                    </table>

                    <div className="flex mt-4 justify-center">
                        {Array.from({length: totalPages}, (_, index) => (
                            <button
                                key={index + 1}
                                onClick={() => handlePageChange(index + 1)}
                                className={`mx-1 px-2 py-1 my-1 border rounded ${
                                    currentPage === index + 1 ? 'bg-gray-700 text-white' : null
                                }`}
                            >
                                {index + 1}
                            </button>
                        ))}
                    </div>
                </div>
            ) : (
                <p>Loading properties...</p>
            )}
        </div>
    );
}
