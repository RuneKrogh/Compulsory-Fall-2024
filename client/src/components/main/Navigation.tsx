import { useState, useRef, useEffect } from "react";
import { Link } from "react-router-dom";
import ThemeSwitcher from "./ThemeSwitcher.tsx";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faHome, faShoppingCart, faUserTie } from '@fortawesome/free-solid-svg-icons';

export default function Navigation() {
    const [isDropdownOpen, setIsDropdownOpen] = useState(false);
    const dropdownRef = useRef(null);

    const toggleDropdown = () => {
        setIsDropdownOpen(prev => !prev);
    };

    const closeDropdown = () => {
        setIsDropdownOpen(false);
    };

    const handleClickOutside = (event) => {
        // @ts-ignore
        if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
            closeDropdown();
        }
    };

    useEffect(() => {
        document.addEventListener("mousedown", handleClickOutside);
        return () => {
            document.removeEventListener("mousedown", handleClickOutside);
        };
    }, []);

    return (
        <div className="navbar bg-base-200 h-16 min-h-[4rem] relative">
            <div className="flex-1">
                <Link to="/" className="btn btn-ghost text-lg">
                    <FontAwesomeIcon icon={faHome} className="text-xl" />
                </Link>
            </div>
            <div className="flex-none">
                <div className="dropdown dropdown-end" ref={dropdownRef}>
                    <label
                        tabIndex={0}
                        className="btn btn-ghost text-lg"
                        onClick={toggleDropdown}
                    >
                        <FontAwesomeIcon icon={faUserTie} className="text-xl" />
                    </label>
                    {isDropdownOpen && (
                        <ul
                            tabIndex={0}
                            className="dropdown-content menu p-2 shadow bg-base-100 rounded-box w-52 z-50"
                        >
                            <li>
                                <Link
                                    to="/customers"
                                    className="hover:bg-gray-600"
                                    onClick={closeDropdown}
                                >
                                    Customers
                                </Link>
                            </li>
                            <li>
                                <Link
                                    to="/orders"
                                    className="hover:bg-gray-600"
                                    onClick={closeDropdown}
                                >
                                    Orders
                                </Link>
                            </li>
                            <li>
                                <Link
                                    to="/products-properties"
                                    className="hover:bg-gray-600"
                                    onClick={closeDropdown}
                                >
                                    Products & Properties
                                </Link>
                            </li>
                        </ul>
                    )}
                </div>

                <Link to="/customer-dashboard" className="btn btn-ghost text-lg">
                    <FontAwesomeIcon icon={faUser} className="text-xl" />
                </Link>

                <Link to="/shopping-cart" className="btn btn-ghost text-lg">
                    <FontAwesomeIcon icon={faShoppingCart} className="text-xl" />
                </Link>
            </div>
        </div>
    );
}
