import React from 'react';
import { Outlet } from 'react-router-dom';
import Navbar from '../Pages/Navbar';

export const DashboardLayout = () => {
    return (
        <div className="d-flex">
            <div style={{ width: '250px', backgroundColor: '#f8f9fa' }}>
                <Navbar />
            </div>
            <div style={{ flex: 1, padding: '20px' }}>
                <Outlet /> {/* Page content will render here */}
            </div>
        </div>
    );
};
