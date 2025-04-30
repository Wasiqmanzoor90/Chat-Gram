import React, { createContext } from 'react'
import axios from 'axios';

import App from "../App";

export const context = createContext();

const Store = ()=> {
const handleRegister =async (e, form)=>{
e.preventDefault();
try {
    
    const res = await  axios.post('https://localhost:7023/api/User/Login', form)
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
}
const signin=async (e, form)=>{

    e.preventDefault();
    try {
        const res = await axios.post('https://localhost:7023/api/User/Register', form)
        if (res.status === 200) {
            console.log(res.data);
            alert('Login successful');
            // Redirect to the desired page
            window.location.href = '/home';
        } else {
            alert('Login failed');
        }
    } catch (error) {
        console.error('Error during login:', error);
        alert('An error occurred during login. Please try again.');
    }
    }


  return (
    <context.Provider value={{...Store,handleRegister, signin}}>
    <App />
    </context.Provider> 
  )
}

export default Store