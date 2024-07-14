import "../styles/menu.css";
import { Link } from 'react-router-dom';
import React, {useState} from "react";
import CreateProjectForm from './CreateProjectForm';

export default function Menu() {

    const [modalIsOpen, setModalIsOpen] = useState(false);
    
    return (
        <div className="menu-right">
            <button className="create-button" onClick={() => setModalIsOpen(true)}>
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
                <Link to="/projects">
                    <i className="icon-file_open" aria-hidden="true"></i>
                    <span className="link_name">Projects</span>
                </Link>
                </li>
                <li className="my-tasks-link">
                <Link to="/mytasks">
                    <i className="icon-attachment" aria-hidden="true"></i>
                    <span className="link_name">Tasks</span>
                </Link>
                </li>
            </ul>
        </div>
    
    );
}
