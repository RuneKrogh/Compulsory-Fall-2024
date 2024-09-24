import React, { useEffect, useState } from "react";
import { http } from "../../http.ts";
import { useAtom } from "jotai";
import { papersAtom } from "../../atoms/PapersAtom.tsx";

export default function PapersList() {
    const [papers, setPapers] = useAtom(papersAtom);
    const [searchQuery, setSearchQuery] = useState("");
    const [currentPage, setCurrentPage] = useState(1);
    const papersPerPage = 15;

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

    // Filter papers based on search query
    const filteredPapers = papers.filter(paper => {
        const fullName = `${paper.name} ${paper.id}`.toLowerCase(); // Adjust this based on available fields
        return fullName.includes(searchQuery.toLowerCase());
    });

    // Calculate the indices for slicing the filtered papers array
    const indexOfLastPaper = currentPage * papersPerPage;
    const indexOfFirstPaper = indexOfLastPaper - papersPerPage;
    const displayedPapers = filteredPapers.slice(indexOfFirstPaper, indexOfLastPaper);

    // Calculate total pages
    const totalPages = Math.ceil(filteredPapers.length / papersPerPage);

    // Handle page change
    const handlePageChange = (pageNumber) => {
        setCurrentPage(pageNumber);
    };

    return (
        <div className="max-w-full overflow-x-auto p-1">
            <div className="flex items-center">
                <h1 className="text-3xl font-bold pr-4">List of Products</h1>
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
            {papers.length > 0 ? (
                <>
                    <table className="table">
                        <thead>
                        <tr>
                            <th className="text-xl px-2 py-1.5">ID</th>
                            <th className="text-xl px-2 py-1">Name</th>
                            <th className="text-xl px-2 py-1">Stock</th>
                            <th className="text-xl px-2 py-1">Price</th>
                            <th className="text-xl px-2 py-1">Discontinued</th>
                        </tr>
                        </thead>
                        <tbody>
                        {displayedPapers.map(paper => (
                            <tr key={paper.id}>
                                <td className="px-2 py-1.5">{paper.id}</td>
                                <td className="px-2 py-1">{paper.name}</td>
                                <td className="px-2 py-1">{paper.stock}</td>
                                <td className="px-2 py-1">{paper.price}</td>
                                <td className="px-2 py-1">{paper.discontinued ? 'Yes' : 'No'}</td>
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
                                className={`mx-1 px-2.5 py-1 mb-4 -mt-2 border rounded ${
                                    currentPage === index + 1 ? 'bg-gray-700 text-white' : null
                                }`}
                            >
                                {index + 1}
                            </button>
                        ))}
                    </div>
                </>
            ) : (
                <p>Loading papers...</p>
            )}
        </div>
    );
}
