import React, { useState } from 'react';
import "../styles/homeContent.css";
import { Link } from 'react-router-dom';
import ProjectItemForHomePage from './ProjectItemForHomePage';
import TaskItemForHomePage from './TaskItemForHomePage';
import useTasksWithStatus from '../hooks/useTasksWithStatus';
import useProject from '../hooks/useProject';
import CreateProjectForm from './CreateProjectForm';

export default function HomeContent() {
  const projects = useProject([]);
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const tasksWithStatus = useTasksWithStatus();

  return (
    <div className='container-home-auth'>
      <div className='item-in-home'>
        <p>My tasks</p>
        <div className='line-block'></div>
        <div className='mytasks-items'>
          {tasksWithStatus.slice(0, 6).map(task => (
            <Link to={`/mytasks/${task.id}`} key={task.id}>
              <TaskItemForHomePage task={task} />
            </Link>
          ))}
        </div>
        <Link to='/mytasks'>
          <i className="icon-chevron_down" aria-hidden="true"></i>
        </Link>
      </div>
      <div className='item-in-home'>
        <p>My projects</p>
        <div className='line-block'></div>
        <div className='projects-items'>
          <div>
            <button className='more-item' onClick={() => setModalIsOpen(true)}>
              <i className="icon-plus" aria-hidden="true"></i>
            </button>
            <CreateProjectForm isOpen={modalIsOpen} onRequestClose={() => setModalIsOpen(false)} />
          </div>
          {projects.slice(0, 5).map(project => (
            <Link to={`/projects/${project.id}`} key={project.id}>
              <ProjectItemForHomePage project={project} />
            </Link>
          ))}
        </div>
        <Link to='/projects'>
          <i className="icon-chevron_down" aria-hidden="true"></i>
        </Link>
      </div>
    </div>
  );
}
