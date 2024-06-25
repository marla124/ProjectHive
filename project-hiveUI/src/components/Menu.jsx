import "../styles/menu.css";
import React from "react";

export default function Menu() {

    return (
        <div className="menu-right">
            <button className="create-button">
                <i className="icon-pen"></i> Create
            </button>

            <ul className="item-links">
                <li className="home-link">
                <a href="#">
                    <i className="icon-home" aria-hidden="true"></i>
                    <span className="link_name">Home</span>
                    </a>
                </li>
                <li className="create-report-link">
                <a href="#">
                    <i className="icon-sheet" aria-hidden="true"></i>
                    <span className="link_name">Reports</span>
                    </a>
                </li>
                <li className="my-projects-link">
                <a href="#">
                    <i className="icon-file_open" aria-hidden="true"></i>
                    <span className="link_name">Projects</span>
                    </a>
                </li>
                <li className="my-tasks-link">
                <a href="#">
                    <i className="icon-attachment" aria-hidden="true"></i>
                    <span className="link_name">Tasks</span>
                    </a>
                </li>
            </ul>
        </div>
    
    );
}
