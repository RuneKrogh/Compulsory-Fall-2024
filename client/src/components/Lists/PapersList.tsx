import React, { useEffect, useState } from "react";
import { http } from "../../http.ts";
import { useAtom } from "jotai";
import { papersAtom } from "../../atoms/PapersAtom.tsx";
import { customerPageAtom } from "../../atoms/PageAtom.tsx"; // Assuming you want to keep the same pagination atom structure

export default function PapersList() {
    const [papers, setPapers] = useAtom(papersAtom);
    const [currentPage, setCurrentPage] = useAtom(customerPageAtom); // Reusing customer page atom for consistency
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

    // Filter papers based on search query
    const filteredPapers = papers.filter(paper => {
        const fullName = `${paper.name} ${paper.id}`.toLowerCase();
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
            {/* Centering the title and search bar */}
            <div className="flex flex-col items-center mb-4">
                <h1 className="text-2xl font-bold">Papers</h1>
                <input
                    type="text"
                    placeholder="Search..."
                    value={searchQuery}
                    onChange={(e) => {
                        setSearchQuery(e.target.value);
                        setCurrentPage(1); // Reset to page 1 whenever the search query changes
                    }}
                    className="input input-bordered input-sm w-full max-w-xs mt-2"
                />
            </div>
            {papers.length > 0 ? (
                <div className="w-4/5 mx-auto"> {/* Set width to 60% and center it */}
                    <table className="table w-full">
                        <thead>
                        <tr>
                            <th className="text-xl px-1 py-1 text-center">ID</th>
                            {/* Centered text */}
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
                                {/* Centered text */}
                                <td className="px-1 py-1 text-center">{paper.name}</td>
                                <td className="px-1 py-1 text-center">{paper.stock}</td>
                                <td className="px-1 py-1 text-center">{paper.price}</td>
                                <td className="px-1 py-1 text-center">{paper.discontinued ? 'Yes' : 'No'}</td>
                                <td className="px-1 py-1 text-center"> {/* Center buttons */}
                                    <div className="flex justify-center space-x-1"> {/* Adjusted alignment */}
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
                <p>Loading papers...</p>
            )}
        </div>
    );
}
