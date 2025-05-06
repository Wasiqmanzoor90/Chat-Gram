import React, { createContext, useEffect, useState } from 'react';
import axios from 'axios';
import App from "../App";

export const context = createContext();

const Store = () => {
    const [posts, setPosts] = useState([]);
    const [comment, setComment] = useState([]);

    const handleRegister = async (e, form) => {
        e.preventDefault();
        try {
            const res = await axios.post('https://localhost:7023/api/User/Login', form);
            if (res.status === 200) {
                console.log(res.data);
                localStorage.setItem('token', res.data.token);
                localStorage.setItem('userId', res.data.id);
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



    const GetComment = async (postId) => {
        console.log(`Fetching comments for postId: ${postId}`); // Add logging to verify the postId
        const token = localStorage.getItem('token');
        try {
          const res = await axios.get(`https://localhost:7023/api/User/GetComment/${postId}`, {
            headers: {
              Authorization: `Bearer ${token}`
            }
          });
          console.log(res.data); // Check what data is returned from the API
          if (res.status === 200) {
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
      

    const CreateCommment = async (PostId, content) => {
        const token = localStorage.getItem('token');
        const UserId = localStorage.getItem('userId')
        const Name = localStorage.getItem('name')


        const CommmentData = {
            UserId,
            PostId,
            Name,
            Content: content,    
            Created: new Date().toISOString() 
        }
        try {

            const res = await axios.post('https://localhost:7023/api/User/CreateComment',
                CommmentData,
                {
                    headers: {
                        Authorization: `Bearer ${token}`,
                        'Content-Type': 'application/json'
                    }
                }
            );
            if (res.status === 200) {
                localStorage.removeItem('cachedComments'); // clear cache if using it
                await GetComment(); // refresh UI
            }
            else {
                alert('Failed to create comment');
            }

        } catch (error) {
            console.error('Error creating comment:', error);
            alert('An error occurred while creating the comment');
        }

    }


    return (
        <context.Provider value={{ handleRegister, signin, posts, GetPost, comment, GetComment, CreateCommment }}>
            <App />
        </context.Provider>
    );
};

export default Store;
