import React from "react";

const Modal = ({ isOpen, onClose, onSubmit, title, children }) => {
    if (!isOpen) return null;

    const handleSubmit = (e) => {
        e.preventDefault();
        onSubmit();
    };

    return (
        <div className="fixed inset-0 flex items-center justify-center z-50">
            <div className="absolute inset-0 bg-black opacity-50" onClick={onClose} />
            <div className="bg-base-200 rounded-lg shadow-lg p-6 z-10 w-3/4 max-w-md">
                <h2 className="text-xl font-bold mb-4 items-center justify-center flex">{title}</h2>
                <form onSubmit={handleSubmit}>
                    {children}
                    <div className="flex justify-end mt-4">
                        <button type="button" className="btn mr-2" onClick={onClose}>
                            Cancel
                        </button>
                        <button type="submit" className="btn">
                            Submit
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default Modal;
