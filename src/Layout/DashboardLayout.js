import React from 'react';
import { Outlet } from 'react-router-dom';
import Navbar from '../Pages/Navbar';

export const DashboardLayout = () => {
    return (
        <div className="d-flex" style={{ minHeight: '100vh' }}>
            {/* Sidebar */}
            <div style={{ width: '250px', backgroundColor: '#f8f9fa' }}>
                <Navbar />
            </div>

            {/* Main Content */}
            <div style={{ flex: 1, padding: '20px' }}>
                <Outlet />
            </div>
        </div>
    );
};
