import React, { useState } from 'react';
import Navbar from './Navbar';
import "../styles/login.css"
import { Link } from 'react-router-dom';

export default function LoginForm() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  return (
    <div className="block">
      <Navbar />
      <div className="container">
        <div className="login form">
          <header>Login</header>
          <form action="#">
            <input type="text" placeholder="Enter your email" value={email} onChange={(e) => setEmail(e.target.value)} />
            <input type="password" placeholder="Enter your password" value={password} onChange={(e) => setPassword(e.target.value)} />
            <a href="/">Forgot password?</a>
            <input type="button" className="button" value="Login" />
          </form>
          <div className="signup">
            <span className="signup">Don't have an account? 
            <Link to="/signup"><label htmlFor="check"> Signup</label></Link>
            </span>
          </div>
        </div>
      </div>
    </div>
  );
}
