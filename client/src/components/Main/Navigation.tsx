import { Link } from "react-router-dom";
import ThemeSwitcher from "./ThemeSwitcher.tsx";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faHome, faShoppingCart, faUserTie } from '@fortawesome/free-solid-svg-icons';

export default function Navigation() {
    return (
        <div className="navbar bg-base-200 h-16 min-h-[4rem]">
            <div className="flex-1">
                <Link to="/" className="btn btn-ghost text-lg">
                    <FontAwesomeIcon icon={faHome} className="text-xl" />
                </Link>
            </div>
            <div className="flex-none">
                <Link to="/shopping-cart" className="btn btn-ghost text-lg">
                    <FontAwesomeIcon icon={faShoppingCart} className="text-xl" />
                </Link>

                <Link to="/admin-dashboard" className="btn btn-ghost text-lg">
                    <FontAwesomeIcon icon={faUserTie} className="text-xl" />
                </Link>

                <Link to="/customer-dashboard" className="btn btn-ghost text-lg">
                    <FontAwesomeIcon icon={faUser} className="text-xl" />
                </Link>
            </div>
        </div>
    );
}
