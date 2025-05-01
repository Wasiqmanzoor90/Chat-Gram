import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { Navigate, Outlet } from 'react-router-dom';

const PrivateRoute = () => {
    const [isVerified, setIsVerified] = useState(null);
    const token = localStorage.getItem('token');

    useEffect(() => {
        const verifyToken = async () => {
            if (!token) {
                setIsVerified(false);
                return;
            }
            try {
                const res = await axios.get('https://localhost:7023/api/User/Verify', {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                });
                setIsVerified(res.status === 200);
            } catch (error) {
                setIsVerified(false);
            }
        };

        verifyToken();
    }, [token]);

    if (isVerified === false) return <Navigate to="/" />;
    if (isVerified === null) return null; // show loader if you want

    return <Outlet />; // show nested route
};

export default PrivateRoute;
