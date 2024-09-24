import React, { useEffect, useState } from "react";
import { http } from "../main/http.ts";
import { useAtom } from "jotai";
import { papersAtom } from "../../atoms/PapersAtom.tsx";
import { customerPageAtom } from "../../atoms/PageAtom.tsx";

export default function PapersList() {
    const [papers, setPapers] = useAtom(papersAtom);
    const [currentPage, setCurrentPage] = useAtom(customerPageAtom);
    const papersPerPage = 15;
    const [searchQuery, setSearchQuery] = useState("");

    useEffect(() => {
        // Only fetch papers if the atom is empty
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

    const handleAddPaper = () => {
        console.log("Add Paper button clicked");
    };

    return (
        <div className="max-w-full overflow-x-auto p-1">
            {}
            <div className="flex flex-col items-center mb-4">
                <h1 className="text-2xl font-bold">Papers</h1>
                <div className="flex items-center my-4"> {}
                    <input
                        type="text"
                        placeholder="Search..."
                        value={searchQuery}
                        onChange={(e) => {
                            setSearchQuery(e.target.value);
                            setCurrentPage(1); // Reset to page 1 whenever the search query changes
                        }}
                        className="input input-bordered input-sm w-full max-w-xs mr-2"
                    />
                    <button
                        onClick={handleAddPaper}
                        className="btn btn-sm"
                    >
                        Add Paper
                    </button>
                </div>
            </div>
            {papers.length > 0 ? (
                <div className="w-4/5 mx-auto"> {}
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

                    {/* Pagination Controls */}
                    <div className="flex mt-4 justify-center">
                        {Array.from({ length: totalPages }, (_, index) => (
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
                <p>Loading papers...</p>
            )}
        </div>
    );
}
