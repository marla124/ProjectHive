import React, { useEffect, useState } from 'react';
import Navbar from './Navbar';
import Menu from './Menu';
import "../styles/projects.css";
import ProjectItem from './ProjectItem';
import { Link } from 'react-router-dom';
import useProjectsWithStatus from '../hooks/useProjectsWithStatus';

export default function ProjectsListPage() {
  const projectsWithStatus = useProjectsWithStatus([]);

  return (
    <div className='page-container'>
      <Navbar />
      <div className='projects-container'>
        <Menu />
        <div className='projects-list'>
          <h1 className='title-header-projects'>My Projects</h1>
          <div className='list'>
            {(projectsWithStatus || []).map(project => (
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
