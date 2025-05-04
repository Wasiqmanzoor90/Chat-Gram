import React, { createContext, useEffect, useState } from 'react';
import axios from 'axios';
import App from "../App";

export const context = createContext();

const Store = () => {
    const [posts, setPosts] = useState([]);
    const [comment, setComment]=useState([]);

    const handleRegister = async (e, form) => {
        e.preventDefault();
        try {
            const res = await axios.post('https://localhost:7023/api/User/Login', form);
            if (res.status === 200) {
                console.log(res.data);
                localStorage.setItem('token', res.data.token);
                localStorage.setItem('userId', res.data.userId);
                localStorage.setItem('name', res.data.name);
                alert('Login successful');
                window.location.href = '/home';
            } else {
                alert('Login failed');
            }
        } catch (error) {
            console.error('Error during login:', error);
            alert('An error occurred during login. Please try again.');
        }
    };

    const signin = async (e, form) => {
        e.preventDefault();
        try {
            const res = await axios.post('https://localhost:7023/api/User/Register', form);
            if (res.status === 200) {
                console.log(res.data);
                alert('Login successful');
                window.location.href = '/home';
            } else {
                alert('Login failed');
            }
        } catch (error) {
            console.error('Error during login:', error);
            alert('An error occurred during login. Please try again.');
        }
    };

    const GetPost = async () => {
        try {
            const token = localStorage.getItem('token'); // get the saved token

            const res = await axios.get('https://localhost:7023/api/User/GetPostsByUser', {
                headers: {
                    Authorization: `Bearer ${token}` // send the token
                }
            });

            if (res.status === 200) {
                console.log(res.data);
                setPosts(res.data);
            }
        } catch (error) {
            console.log('Error fetching posts:', error);
            if (error.response?.status === 401) {
                alert('Unauthorized. Please log in again.');
                localStorage.clear();
                window.location.href = '/login';
            }
        }
    };

    //  Fetch posts on component mount
    useEffect(() => {
        GetPost();
    }, []);



    const GetComment = async () => {
        const token = localStorage.getItem('token');
        try {
            const res = await axios.get('https://localhost:7023/api/User/GetComment', {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            console.log(res); // Log the full response to see what you're getting
            if (res.status === 200) {
                console.log('Comments:', res.data);
                setComment(res.data);
            }
        } catch (error) {
            console.log('Error fetching comments:', error.response ? error.response.data : error.message);
            if (error.response?.status === 401) {
                alert('Unauthorized. Please log in again.');
                localStorage.clear();
                window.location.href = '/login';
            }
        }
    };
    
    useEffect(() => {
        console.log("Fetching comments...");
        GetComment();
    }, []);
    return (
        <context.Provider value={{ handleRegister, signin, posts, GetPost, comment, GetComment }}>
            <App />
        </context.Provider>
    );
};

export default Store;
