import React, { useEffect, useState } from "react";
import { http } from "../main/http.ts";
import { useAtom } from "jotai";
import { propertiesAtom } from "../../atoms/PropertiesAtom.tsx";
import Modal from "../modals/Modal.tsx";

export default function PropertiesList() {
    const [properties, setProperties] = useAtom(propertiesAtom);
    const [searchQuery, setSearchQuery] = useState("");
    const [currentPage, setCurrentPage] = useState(1);
    const propertiesPerPage = 15;
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [editingProperty, setEditingProperty] = useState(null);
    const [newProperty, setNewProperty] = useState({ propertyName: "" });

    // Fetch properties from the API
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

    // Filter properties based on search query
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

    // Handle opening the modal for adding or editing
    const handleAddProperty = () => {
        setNewProperty({ propertyName: "" });
        setEditingProperty(null);
        setIsModalOpen(true);
    };

    const handleEditProperty = (property) => {
        setNewProperty(property);
        setEditingProperty(property.id);
        setIsModalOpen(true);
    };

    const handleDeleteProperty = (propertyId) => {
        if (window.confirm("Are you sure you want to delete this property?")) {
            http.api.propertyDeleteProperty(propertyId)
                .then(() => {
                    const updatedProperties = properties.filter(property => property.id !== propertyId);
                    setProperties(updatedProperties);
                })
                .catch(error => {
                    console.error('Error deleting property:', error);
                });
        }
    };

    const handleModalSubmit = () => {
        if (editingProperty) {
            // Update an existing property
            http.api.propertyUpdateProperty(editingProperty, newProperty)
                .then(response => {
                    const updatedProperties = properties.map(property =>
                        property.id === editingProperty
                            ? { ...property, ...newProperty }
                            : property
                    );
                    setProperties(updatedProperties);
                    setIsModalOpen(false);
                })
                .catch(error => {
                    console.error('Error updating property:', error);
                });
        } else {
            // Add a new property
            http.api.propertyAddProperty(newProperty)
                .then(response => {
                    setProperties([...properties, response.data]);
                    setIsModalOpen(false);
                })
                .catch(error => {
                    console.error('Error adding property:', error);
                });
        }

        // Reset form
        setNewProperty({ propertyName: "" });
    };

    return (
        <div className="max-w-full overflow-x-auto p-1">
            {/* Page Header */}
            <div className="flex flex-col items-center mb-4">
                <h1 className="text-2xl font-bold">Properties</h1>
                <div className="flex items-center my-4">
                    <input
                        type="text"
                        placeholder="Search..."
                        value={searchQuery}
                        onChange={(e) => {
                            setSearchQuery(e.target.value);
                            setCurrentPage(1); // Reset to page 1 on search
                        }}
                        className="input input-bordered input-sm w-full max-w-xs mr-2"
                    />
                    <button onClick={handleAddProperty} className="btn btn-sm">
                        Add Property
                    </button>
                </div>
            </div>

            {/* Table to display properties */}
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
                                        <button className="btn btn-sm" onClick={() => handleEditProperty(property)}>
                                            Edit
                                        </button>
                                        <button className="btn btn-sm btn-danger" onClick={() => handleDeleteProperty(property.id)}>
                                            Delete
                                        </button>
                                    </div>
                                </td>
                            </tr>
                        ))}
                        </tbody>
                    </table>

                    {/* Pagination */}
                    <div className="flex mt-4 justify-center">
                        {Array.from({ length: totalPages }, (_, index) => (
                            <button
                                key={index + 1}
                                onClick={() => handlePageChange(index + 1)}
                                className={`mx-1 px-2 py-1 my-1 border rounded ${currentPage === index + 1 ? 'bg-gray-700 text-white' : null}`}
                            >
                                {index + 1}
                            </button>
                        ))}
                    </div>
                </div>
            ) : (
                <p>Loading properties...</p>
            )}

            {/* Add/Edit Property Modal */}
            <Modal
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                onSubmit={handleModalSubmit}
                title={editingProperty ? "Edit Property" : "Add New Property"}
            >
                <div>
                    <label className="block mb-2">Property Name</label>
                    <input
                        type="text"
                        value={newProperty.propertyName}
                        onChange={(e) => setNewProperty({ ...newProperty, propertyName: e.target.value })}
                        className="input input-bordered w-full mb-4"
                        required
                    />
                </div>
            </Modal>
        </div>
    );
}
