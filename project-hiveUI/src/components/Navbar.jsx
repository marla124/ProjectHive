import { Link } from 'react-router-dom';
import "../styles/navbar.css";
import "../assets//mfglabs-iconset-master/css/mfglabs_iconset.css"
import React from "react";

export default function Navbar() {

    return (
        <div className="header_menu">
            <div className="logo">
            <Link to="/" className='logo-text'>
                <h1>ProjectHive</h1>
            </Link>
            </div>
            <div className="button_items">
                <Link to="/search" className="search_butt">
                    <i className="icon-magnifying" aria-hidden="true"></i>
                </Link>
                <Link to="/notifications" className="notifications_butt">
                    <i className="icon-ringbell" aria-hidden="true"></i>
                </Link>
                <Link to="/login" className="login_butt">
                    <i className="icon-user" aria-hidden="true"></i>
                    Log In
                </Link>
            </div>
        </div>
    );
}
