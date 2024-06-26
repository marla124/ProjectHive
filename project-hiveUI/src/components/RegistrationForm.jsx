import React, { useState } from 'react';
import Navbar from './Navbar';
import "../styles/register.css";
import { Link, useNavigate } from 'react-router-dom';
import axios from "axios";

export default function RegistrationForm() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [passwordConfirmation, setPasswordConfirmation] = useState('');
  const navigate = useNavigate();
  
  const handleSubmit = async (event) => {
    event.preventDefault();
    const data = { email, password, passwordConfirmation }; 
    try {
    const response= await 
    axios.post("http://localhost:5183/api/User/CreateUser", data);
      if (response.status === 201 || response.status === 200) {
        navigate('/login');
      }
    }
    catch(error) {
        console.log(error);
      };
  };

  return (
    <div className="block">
      <Navbar />
      <div className="registration-container">
        <div className="registration form">
          <header>Signup</header>
          <form onSubmit={handleSubmit}>
            <input type="email" placeholder="Enter your email" value={email} onChange={(e) => setEmail(e.target.value)} required/>
            <input type="password" placeholder="Create a password" value={password} onChange={(e) => setPassword(e.target.value)} required minLength="8" maxLength="64"/>
            <input type="password" placeholder="Confirm your password" value={passwordConfirmation} onChange={(e) => setPasswordConfirmation(e.target.value)} required/>
            <input type="submit" className="button" value="Signup" />
          </form>
          <div className="signin">
            <span className="signin">Already have an account? 
              <Link to="/login"><label htmlFor="check"> Login</label></Link>
            </span>
          </div>
        </div>
      </div>
    </div>
  );
}