import React, { useEffect, useState } from 'react';
import Navbar from './Navbar';
import Menu from './Menu';
import "../styles/projects.css";
import useProject from '../hooks/useProject';
import ProjectItem from './ProjectItem';
import { Link } from 'react-router-dom';

export default function ProjectsListPage() {
  const projects = useProject([]);

  return (
    <div className='page-container'>
      <Navbar />
      <div className='projects-container'>
        <Menu />
        <div className='projects-list'>
          <h1 className='title-header-projects'>My Projects</h1>
          <div className='list'>
            {projects.map(project => (
              <Link to={`/projects/${project.id}`} key={project.id}>
                <ProjectItem project={project} />
              </Link>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
