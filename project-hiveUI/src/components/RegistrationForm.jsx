import React, { useState } from 'react';
import Navbar from './Navbar';
import "../styles/register.css";
import { Link } from 'react-router-dom';
import axios from "axios";

export default function RegistrationForm() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [response, setResponse] = useState(""); 

  const handleSubmit = (event) => {
    event.preventDefault();
    const data = { email, password }; 
    axios
      .post("http://localhost:5183/api/User/CreateUser", data, {
        headers: {
        'Content-Type': 'application/json'
      },
    })
      .then((response) => {
        setResponse(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  return (
    <div className="block">
      <Navbar />
      <div className="registration-container">
        <div className="registration form">
          <header>Signup</header>
          <form onSubmit={handleSubmit}>
            <input type="email" placeholder="Enter your email" value={email} onChange={(e) => setEmail(e.target.value)} required/>
            <input type="password" placeholder="Create a password" value={password} onChange={(e) => setPassword(e.target.value)} required minlength="8" maxlength="64"/>
            <input type="password" placeholder="Confirm your password" value={confirmPassword} onChange={(e) => setConfirmPassword(e.target.value)} required/>
            <input type="submit" className="button" value="Signup" />
          </form>
          <div className="signin">
            <span className="signin">Already have an account? 
              <Link to="/login"><label htmlFor="check"> Login</label></Link>
            </span>
          </div>
        </div>
      </div>
      {response && (
        <p>
          Данные успешно отправлены: ({response.email})
        </p>
      )}
    </div>
  );
}