import {Route, Routes} from "react-router-dom";
import React, {useEffect} from "react";
import {Toaster} from "react-hot-toast";
import {DevTools} from "jotai-devtools";
import Navigation from "./Navigation.tsx";
import {useAtom} from "jotai";
import {ThemeAtom} from "../../atoms/ThemeAtom.tsx";
import Home from "./Home.tsx";
import AdminDashboard from "../Dashboards/AdminDashboard.tsx";

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
            <Route path="/shopping-cart" element={null}/>
            <Route path="/customer-dashboard" element={null}/>
            <Route path="/admin-dashboard" element={<AdminDashboard/>}/>
        </Routes>
        <DevTools/>

    </>)
}
export default App;