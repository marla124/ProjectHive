import React, { useState } from 'react';
import Navbar from './Navbar';
import "../styles/register.css";
import { Link } from 'react-router-dom';

export default function RegistrationForm() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');

  return (
    <div className="block">
      <Navbar />
      <div className="container">
        <div className="registration form">
          <header>Signup</header>
          <form action="#">
            <input type="text" placeholder="Enter your email" value={email} onChange={(e) => setEmail(e.target.value)} />
            <input type="password" placeholder="Create a password" value={password} onChange={(e) => setPassword(e.target.value)} />
            <input type="password" placeholder="Confirm your password" value={confirmPassword} onChange={(e) => setConfirmPassword(e.target.value)} />
            <input type="button" className="button" value="Signup" />
          </form>
          <div className="signup">
            <span className="signup">Already have an account? 
              <Link to="/login"><label htmlFor="check"> Login</label></Link>
            </span>
          </div>
        </div>
      </div>
    </div>
  );
}
