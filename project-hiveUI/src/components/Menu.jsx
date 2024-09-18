import "../styles/menu.css";
import { Link, useNavigate } from 'react-router-dom';
import React, { useState } from "react";
import CreateProjectForm from './CreateProjectForm';
import useUserAuthentication from '../hooks/useUserAuthentication';

export default function Menu() {

    const [modalIsOpen, setModalIsOpen] = useState(false);
    const navigate = useNavigate();
    const isUserLoggedIn = useUserAuthentication(false);

    const handleButtonClick = () => {
        if (isUserLoggedIn) {
            setModalIsOpen(true);
        } else {
            navigate('/login');
        }
    };
    return (
        <div className="menu-right">
            <button className="create-button" onClick={handleButtonClick}>
                <i className="icon-pen"></i> Create
            </button>
            <CreateProjectForm isOpen={modalIsOpen} onRequestClose={() => setModalIsOpen(false)} />
            <ul className="item-links">
                <li className="home-link">
                    <Link to="/">
                        <i className="icon-home" aria-hidden="true"></i>
                        <span className="link_name">Home</span>
                    </Link>
                </li>
                <li className="create-report-link">
                    <a href="#">
                        <i className="icon-sheet" aria-hidden="true"></i>
                        <span className="link_name">Reports</span>
                    </a>
                </li>
                <li className="my-projects-link">
                    <Link to={isUserLoggedIn ? '/projects' : '/login'} >
                        <i className="icon-file_open" aria-hidden="true"></i>
                        <span className="link_name">Projects</span>
                    </Link>
                </li>
                <li className="my-tasks-link">
                    <Link to={isUserLoggedIn ? '/mytasks' : '/login'} >

                        <i className="icon-attachment" aria-hidden="true"></i>
                        <span className="link_name">Tasks</span>
                    </Link>
                </li>
            </ul>
        </div>

    );
}
