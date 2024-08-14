import { Link, useNavigate } from 'react-router-dom';
import "../styles/navbar.css";
import "../assets/mfglabs-iconset-master/css/mfglabs_iconset.css";
import React, { useEffect, useState } from "react";
import SearchForm from './SearchForm';
import axios from 'axios';

export default function Navbar({ onLogout }) {
    const [isUserLoggedIn, setIsUserLoggedIn] = useState(false);
    const [modalIsOpen, setModalIsOpen] = useState(false);
    const navigate = useNavigate();

    const handleButtonClick = () => {
        if (isUserLoggedIn) {
            setModalIsOpen(true);
        } else {
            navigate('/login');
        }
    };

    useEffect(() => {
        const token = localStorage.getItem('jwtToken');
        if (token) {
            setIsUserLoggedIn(true);
        }
    }, []);

    const handleLogout = async () => {
        const refreshToken = localStorage.getItem('refreshToken');
        if (refreshToken) {
            try {
                await axios.delete(`/api/Token/Revoke/${refreshToken}`, {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                });
                localStorage.removeItem('jwtToken');
                localStorage.removeItem('refreshToken');
                setIsUserLoggedIn(false);
                onLogout();
                navigate('/');
            } catch (error) {
                console.error('Error revoking token:', error);
            }
        }
    };

    return (
        <div className="header_menu">
            <div className="logo">
                <Link to="/" className='logo-text'>
                    <h1>ProjectHive</h1>
                </Link>
            </div>
            <div className="button_items">
                <SearchForm isOpen={modalIsOpen} onRequestClose={() => setModalIsOpen(false)} />
                <button className="search_butt" onClick={handleButtonClick}>
                    <i className="icon-magnifying" aria-hidden="true"></i>
                </button>
                <Link to="/notifications" className="notifications_butt">
                    <i className="icon-ringbell" aria-hidden="true"></i>
                </Link>
                {isUserLoggedIn ? (
                    <button onClick={handleLogout} className="login_butt">
                        <i className="icon-user" aria-hidden="true"></i>
                        Log Out
                    </button>
                ) : (
                    <Link to="/login" className="login_butt">
                        <i className="icon-user" aria-hidden="true"></i>
                        Log In
                    </Link>
                )}
            </div>
        </div>
    );
}
