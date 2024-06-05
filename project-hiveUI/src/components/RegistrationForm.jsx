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
      .post("/api/User/CreateUser", data, {
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
      <div className="container">
        <div className="registration form">
          <header>Signup</header>
          <form onSubmit={handleSubmit}>
            <input type="text" placeholder="Enter your email" value={email} onChange={(e) => setEmail(e.target.value)} />
            <input type="password" placeholder="Create a password" value={password} onChange={(e) => setPassword(e.target.value)} />
            <input type="password" placeholder="Confirm your password" value={confirmPassword} onChange={(e) => setConfirmPassword(e.target.value)} />
            <input type="submit" className="button" value="Signup" />
          </form>
          <div className="signup">
            <span className="signup">Already have an account? 
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