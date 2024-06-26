    import React, { useState } from 'react';
    import Navbar from './Navbar';
    import Menu from './Menu';
    import "../styles/projects.css"
    import ProjectItem from './ProjectItem';

    export default function ProjectsListPage(){
        const [projects, setProjects] = useState([
            { id: 1, name: 'Project1' },
            { id: 2, name: 'Project2' },
            { id: 3, name: 'Project3' },
            { id: 4, name: 'Project4' },
            { id: 5, name: 'Project5' },
            { id: 6, name: 'Project6' },
        ]);

        return(
        <div className='page-container'>
            <Navbar />
            <div className='projects-container'>
            <Menu />
            <div className='projects-list'>
                <h1 className='title-header'>My Projects</h1>
                <div className='list'>
                    {projects.map(project => 
                        <ProjectItem project={project} key ={project.id}/>
                    )}
                </div>
            </div>
        </div>  
        </div>
        );
    }