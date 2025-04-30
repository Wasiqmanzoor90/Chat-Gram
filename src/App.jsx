import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Login from './Pages/Login';
import Register from './Pages/Register';
import Home from './Pages/Home'; // Example page
import { DashboardLayout } from './Layout/DashboardLayout'; // Layout
import Footerdown from './Pages/Footerdown';

function App() {
    return (
        <BrowserRouter>
            <Routes>
                {/* Routes without Navbar */}
                <Route path="/" element={<Login />} />
                <Route path="/register" element={<Register />} />

                {/* Routes with Navbar */}
                <Route element={<DashboardLayout />}>
                    <Route path="/home" element={<Home />} />
                </Route>
            </Routes>
            <Footerdown/>
        </BrowserRouter>
    );
}

export default App;
