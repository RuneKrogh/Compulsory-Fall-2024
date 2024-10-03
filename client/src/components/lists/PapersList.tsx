import React, { useEffect, useState } from "react";
import { http } from "../main/http.ts";
import { useAtom } from "jotai";
import { papersAtom } from "../../atoms/PapersAtom.tsx";
import { customerPageAtom } from "../../atoms/PageAtom.tsx";
import Modal from "../modals/Modal.tsx";

export default function PapersList() {
    const [papers, setPapers] = useAtom(papersAtom);
    const [currentPage, setCurrentPage] = useAtom(customerPageAtom);
    const papersPerPage = 15;
    const [searchQuery, setSearchQuery] = useState("");
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [editingPaper, setEditingPaper] = useState(null); // To edit existing paper
    const [newPaper, setNewPaper] = useState({ name: "", stock: 0, price: 0, discontinued: false }); // Form data

    // Fetch papers if the list is empty
    useEffect(() => {
        if (papers.length === 0) {
            http.api.paperGetAllPapers()
                .then(response => {
                    setPapers(response.data);
                })
                .catch(error => {
                    console.error('Error fetching papers:', error);
                });
        }
    }, [papers, setPapers]);

    // Filter papers based on search query
    const filteredPapers = papers.filter(paper => {
        const fullName = `${paper.name} ${paper.id}`.toLowerCase();
        return fullName.includes(searchQuery.toLowerCase());
    });

    const indexOfLastPaper = currentPage * papersPerPage;
    const indexOfFirstPaper = indexOfLastPaper - papersPerPage;
    const displayedPapers = filteredPapers.slice(indexOfFirstPaper, indexOfLastPaper);

    const totalPages = Math.ceil(filteredPapers.length / papersPerPage);

    const handlePageChange = (pageNumber) => {
        setCurrentPage(pageNumber);
    };

    // Open the modal to add a new paper
    const handleAddPaper = () => {
        setNewPaper({ name: "", stock: 0, price: 0, discontinued: false });
        setEditingPaper(null);
        setIsModalOpen(true);
    };

    // Open the modal to edit an existing paper
    const handleEditPaper = (paper) => {
        setNewPaper(paper);
        setEditingPaper(paper.id);
        setIsModalOpen(true);
    };

    const handleDeletePaper = (paperId) => {
        if (window.confirm("Are you sure you want to delete this paper?")) {
            http.api.paperDeletePaper(paperId)
                .then(() => {
                    setPapers(papers.filter(paper => paper.id !== paperId));
                })
                .catch(error => {
                    console.error('Error deleting paper:', error);
                });
        }
    };

    const handleModalSubmit = () => {
        if (editingPaper) {
            // Update existing paper
            http.api.paperUpdatePaper(editingPaper, newPaper)
                .then(response => {
                    setPapers(papers.map(paper => paper.id === editingPaper ? { ...paper, ...newPaper } : paper));
                    setIsModalOpen(false);
                })
                .catch(error => {
                    console.error('Error updating paper:', error);
                });
        } else {
            // Add new paper
            http.api.paperAddPaper(newPaper)
                .then(response => {
                    setPapers([...papers, response.data]);
                    setIsModalOpen(false);
                })
                .catch(error => {
                    console.error('Error adding paper:', error);
                });
        }
    };

    return (
        <div className="max-w-full overflow-x-auto p-1">
            <div className="flex flex-col items-center mb-4">
                <h1 className="text-2xl font-bold">Papers</h1>
                <div className="flex items-center my-4">
                    <input
                        type="text"
                        placeholder="Search..."
                        value={searchQuery}
                        onChange={(e) => {
                            setSearchQuery(e.target.value);
                            setCurrentPage(1);
                        }}
                        className="input input-bordered input-sm w-full max-w-xs mr-2"
                    />
                    <button onClick={handleAddPaper} className="btn btn-sm">Add Paper</button>
                </div>
            </div>
            {papers.length > 0 ? (
                <div className="w-4/5 mx-auto">
                    <table className="table w-full">
                        <thead>
                        <tr>
                            <th className="text-xl px-1 py-1 text-center">ID</th>
                            <th className="text-xl px-1 py-1 text-center">Name</th>
                            <th className="text-xl px-1 py-1 text-center">Stock</th>
                            <th className="text-xl px-1 py-1 text-center">Price</th>
                            <th className="text-xl px-1 py-1 text-center">Discontinued</th>
                            <th className="text-xl px-1 py-1 text-center">Actions</th>
                        </tr>
                        </thead>
                        <tbody>
                        {displayedPapers.map(paper => (
                            <tr key={paper.id}>
                                <td className="px-1 py-1 text-center">{paper.id}</td>
                                <td className="px-1 py-1 text-center">{paper.name}</td>
                                <td className="px-1 py-1 text-center">{paper.stock}</td>
                                <td className="px-1 py-1 text-center">{paper.price}</td>
                                <td className="px-1 py-1 text-center">{paper.discontinued ? 'Yes' : 'No'}</td>
                                <td className="px-1 py-1 text-center">
                                    <div className="flex justify-center space-x-1">
                                        <button className="btn btn-sm" onClick={() => handleEditPaper(paper)}>Edit</button>
                                        <button className="btn btn-sm btn-danger" onClick={() => handleDeletePaper(paper.id)}>Delete</button>
                                    </div>
                                </td>
                            </tr>
                        ))}
                        </tbody>
                    </table>
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
                <p>Loading papers...</p>
            )}

            {/* Add/Edit Paper Modal */}
            <Modal
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                onSubmit={handleModalSubmit}
                title={editingPaper ? "Edit Paper" : "Add New Paper"}
            >
                <div>
                    <label className="block mb-2">Name</label>
                    <input
                        type="text"
                        value={newPaper.name}
                        onChange={(e) => setNewPaper({ ...newPaper, name: e.target.value })}
                        className="input input-bordered w-full mb-4"
                        required
                    />
                </div>
                <div>
                    <label className="block mb-2">Stock</label>
                    <input
                        type="number"
                        value={newPaper.stock}
                        onChange={(e) => setNewPaper({ ...newPaper, stock: parseInt(e.target.value) })}
                        className="input input-bordered w-full mb-4"
                        required
                    />
                </div>
                <div>
                    <label className="block mb-2">Price</label>
                    <input
                        type="number"
                        step="0.01"
                        value={newPaper.price}
                        onChange={(e) => setNewPaper({ ...newPaper, price: parseFloat(e.target.value) })}
                        className="input input-bordered w-full mb-4"
                        required
                    />
                </div>
                <div>
                    <label className="block mb-2">Discontinued</label>
                    <select
                        value={newPaper.discontinued ? "Yes" : "No"}
                        onChange={(e) => setNewPaper({ ...newPaper, discontinued: e.target.value === "Yes" })}
                        className="select select-bordered w-full mb-4"
                    >
                        <option value="No">No</option>
                        <option value="Yes">Yes</option>
                    </select>
                </div>
            </Modal>
        </div>
    );
}
