import { Link } from 'react-router-dom';
import "../styles/navbar.css";
import "../assets//mfglabs-iconset-master/css/mfglabs_iconset.css"
import React from "react";
import { useNavigate } from 'react-router-dom';

export default function Navbar() {
    let navigate = useNavigate();

    const goToLogin = () => {
      navigate('/login');
    };
    return (
        <div className="header_menu">
            <div className="logo">
                <h1>ProjectHive</h1>
            </div>
            <div className="button_items">
                <Link to="/search" className="search_butt">
                    <i className="icon-magnifying" aria-hidden="true"></i>
                </Link>
                <Link to="/notifications" className="notifications_butt">
                    <i className="icon-ringbell" aria-hidden="true"></i>
                </Link>
                <Link to="/login" className="login_butt" onClick={goToLogin}>
                    <i className="icon-user" aria-hidden="true"></i>
                    Log In
                </Link>
            </div>
        </div>
    );
}
