import React, { useState } from 'react';
import Navbar from './Navbar';
import "../styles/login.css"
import { Link } from 'react-router-dom';
import axios from "axios";

export default function LoginForm() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [response, setResponse] = useState(""); 

  const handleSubmit = (event) => {
    event.preventDefault();
    const data = { email, password }; 
    axios
      .post("http://localhost:5183/api/Token/GenerateToken", data)
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
        <div className="login form">
          <header>Login</header>
          <form onSubmit={handleSubmit}>
            <input type="text" placeholder="Enter your email" value={email} onChange={(e) => setEmail(e.target.value)} />
            <input type="password" placeholder="Enter your password" value={password} onChange={(e) => setPassword(e.target.value)} />
            <a href="/">Forgot password?</a>
            <input type="submit" className="button" value="Login" /> {/* Изменение типа кнопки на "submit" */}
          </form>
          <div className="signup">
            <span className="signup">Don't have an account? 
            <Link to="/signup"><label htmlFor="check"> Signup</label></Link>
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