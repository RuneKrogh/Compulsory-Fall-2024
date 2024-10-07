import {Route, Routes} from "react-router-dom";
import React, {useEffect} from "react";
import {Toaster} from "react-hot-toast";
import {DevTools} from "jotai-devtools";
import Navigation from "./Navigation.tsx";
import {useAtom} from "jotai";
import {ThemeAtom} from "../../atoms/ThemeAtom.tsx";
import Home from "../dashboards/Home.tsx";
import Customers from "../dashboards/Customers.tsx";
import Orders from "../dashboards/Orders.tsx";
import ProductsProperties from "../dashboards/ProductsProperties.tsx";
import ShoppingCart from "../dashboards/ShoppingCart.tsx";
import customer from "../dashboards/Customer.tsx";
import Customer from "../dashboards/Customer.tsx";

const App = () => {

    const [theme, setTheme] = useAtom(ThemeAtom);

    useEffect(() => {
        localStorage.setItem('theme', theme);
        document.documentElement.setAttribute('data-theme', theme);
    }, [theme])

    return (<>
        <Navigation/>
        <Toaster position={"bottom-right"}/>
        <Routes>
            <Route path="/" element={<Home/>}/>
            <Route path="/shopping-cart" element={<ShoppingCart/>}/>
            <Route path="/customer-dashboard" element={null}/>

            // Admin Stuff
            <Route path="/customers" element={<Customers/>}/>
            <Route path="/orders" element={<Orders/>}/>
            <Route path="/products-properties" element={<ProductsProperties/>}/>
        </Routes>

    </>)
}
export default App;