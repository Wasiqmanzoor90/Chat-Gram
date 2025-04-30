import React from 'react';

import screenshot from '../Root/Img/Screenshot (181).png';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFacebookF } from '@fortawesome/free-brands-svg-icons';
import { context } from '../Context/Store';
import { Link } from 'react-router-dom';


function Login() {

    const [email, setEmail] = React.useState('');
    const [password, setPassword] = React.useState('');
    const form = {
        email,
        password
    }


    const { handleRegister } = React.useContext(context);

    return (
        <div className="d-flex justify-content-center align-items-center " style={{ border: '1px solid black', minHeight: '100vh' }}>
            <div className='me-5'>
                <img src={screenshot} alt="Login" style={{ width: '600px' }} />
            </div>
            <div>
                <div className='text-center'>
                    <span
                        className='text-center '
                        style={{
                            fontFamily: '"Poppins", sans-serif', // Use the Poppins font
                            fontSize: '28px', // Font size
                            fontWeight: '400', // Normal weight
                            lineHeight: '32px', // Line height
                            color: 'rgb(38, 38, 38)', // Text color
                        }}
                    >
                        Instagram
                    </span>
                </div>
                <div className="container p-lg-5" >
                    <div className="row">
                        <div className="col-md-12">

                            <form>
                                <div className="form-group mb-3">

                                    <input placeholder='Phone number, username, or email' type="email" className="form-control" id="email" style={{ backgroundColor: '#f0f0f0', fontSize: '0.8rem', width: '280px' }} value={email} onChange={(e) => { setEmail(e.target.value) }} />
                                </div>
                                <div className="form-group mb-3">

                                    <input placeholder='Password' type="password" className="form-control " style={{ backgroundColor: '#f0f0f0', fontSize: '0.8rem' }} id="password" value={password} onChange={(e) => { setPassword(e.target.value) }} />
                                </div>
                                <button
                                    style={{ opacity: '0.8' }}
                                    onClick={(e) => handleRegister(e, form)}
                                    type="submit"
                                    className="btn btn-primary w-100"
                                >
                                    Login
                                </button>

                            </form>
                            <div className="text-center my-3">
                                <hr style={{ width: '40%', display: 'inline-block', margin: '0 10px' }} />
                                <span>or</span>
                                <hr style={{ width: '40%', display: 'inline-block', margin: '0 10px' }} />
                            </div>
                            <div className="d-flex justify-content-center">
                                <div>
                                    <FontAwesomeIcon icon={faFacebookF} style={{ color: "#5F9BF7", fontSize: '1rem', marginRight: '10px' }} />
                                </div>
                                <div>
                                    <a style={{ textDecoration: 'none', fontWeight: '500', opacity: '0.8' }} href="ok">Login with facebook</a>

                                </div>

                            </div>
                            <div>
                                <div className='text-center mt-4'>
                                    <a className='text-center' style={{ textDecoration: 'none', color: 'black' }} href="">Forget Password?</a>
                                </div>

                                <div className='mt-5 text-center'>
                                    <p>
                                        Don't you have an account?
                                    

                                        <Link  style={{ textDecoration: 'none', color: '#007BFF', fontWeight: 'bold' }} to='/Register'>Sign up</Link>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );

}
export default Login;
