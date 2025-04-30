import React from 'react'
import { context } from '../Context/Store'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faFacebookF } from '@fortawesome/free-brands-svg-icons';
function Register() {

  const [Form, setForm] = React.useState({
    email: '',
    name: '',
    password: ''
  })
  const { signin } = React.useContext(context);
  return (
    <div
      className="container d-flex align-content-center p-4"
      style={{ width: '750px' }}

    >
      <div
        className="container px-4 mt-2 mb-5"
        style={{ border: '1px solid lightgray', width: '50%', margin: 'auto' }}
      >
        <h1 className="text-center m-3">Instagram</h1>
        <p>Sign up to see photos and videos from your friends.</p>
        <button className="btn btn-primary mt-2 w-100" style={{ opacity: '0.8' }}>
          <span>
            <FontAwesomeIcon
              icon={faFacebookF}
              style={{
                color: "#5F9BF7",
                fontSize: '1rem',
                fontWeight: 'bold',
                color: 'white',
                marginRight: '10px',
                verticalAlign: 'middle',
              }}
            />
          </span>
         <b style={{opacity:'0.9'}}>Login with Facebook</b> 
        </button>

        <div className="text-center my-3">
          <hr style={{ width: '40%', display: 'inline-block', margin: '0 10px' }} />
          <span>or</span>
          <hr style={{ width: '40%', display: 'inline-block', margin: '0 10px' }} />
        </div>
        <form onSubmit={(e) => signin(e, Form)}>
          <div className="form-group mb-3">
            <input
              placeholder="Mobile Number or Email"
              style={{ backgroundColor: '#f0f0f0', fontSize: '0.8rem' }}
              type="text"
              className="form-control"
              value={Form.email}
              onChange={(e) => setForm({ ...Form, email: e.target.value })}
            />
          </div>
          <div className="form-group mb-3">
            <input
              placeholder="Username"
              style={{ backgroundColor: '#f0f0f0', fontSize: '0.8rem' }}
              type="text"
              className="form-control"
              value={Form.name}
              onChange={(e) => setForm({ ...Form, name: e.target.value })}
            />
          </div>
          <div className="form-group mb-3">
            <input
              style={{ backgroundColor: '#f0f0f0', fontSize: '0.8rem' }}
              placeholder="Password"
              type="password"
              className="form-control"
              value={Form.password}
              onChange={(e) => setForm({ ...Form, password: e.target.value })}
            />
            <p className='my-3' style={{ fontSize: '0.8rem', opacity: '0.8' }}>People who use our service may have uploaded your contact information to Instagram. <a style={{ textDecoration: 'none' }} href="">Learn More</a> </p>
            <p style={{ fontSize: '0.8rem', opacity: '0.8' }}>By signing up, you agree to our Terms ,<a style={{ textDecoration: 'none' }} href=""> Privacy Policy and Cookies Policy</a> </p>
          </div>
          <button style={{ opacity: '0.8' }} type="submit" className="btn btn-primary w-100 mb-3">Sign Up</button>
        </form>
      </div>
    </div>



  )
}

export default Register