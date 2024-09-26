import React, { useState } from 'react';
import "../styles/homeContent.css";
import { Link } from 'react-router-dom';
import ProjectItemForHomePage from './ProjectItemForHomePage';
import TaskItemForHomePage from './TaskItemForHomePage';
import useTasksWithStatus from '../hooks/useTasksWithStatus';
import useProjectsWithStatus from '../hooks/useProjectsWithStatus';
import CreateProjectForm from './CreateProjectForm';
import TaskForm from './TaskForm';

export default function HomeContent() {
  const projectsWithStatus = useProjectsWithStatus([]);
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [selectedTask, setSelectedTask] = useState(null);
  const [taskModalIsOpen, setTaskModalIsOpen] = useState(false);
  const tasksWithStatus = useTasksWithStatus();

  const handleTaskClick = (task) => {
    setSelectedTask(task);
    setTaskModalIsOpen(true);
  };

  return (
    <div className='container-home-auth'>
      <div className='item-in-home'>
        <p>My tasks</p>
        <div className='line-block'></div>
        <div className='mytasks-items'>
          {(tasksWithStatus || []).slice(0, 6).map(task => (
            <div key={task.id} onClick={() => handleTaskClick(task)}>
              <TaskItemForHomePage task={task} />
            </div>
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
          {(projectsWithStatus || []).slice(0, 5).map(project => (
            <Link to={`/projects/${project.id}`} key={project.id}>
              <ProjectItemForHomePage project={project} />
            </Link>
          ))}
        </div>
        <Link to='/projects'>
          <i className="icon-chevron_down" aria-hidden="true"></i>
        </Link>
      </div>
      {selectedTask && (
        <TaskForm isOpen={taskModalIsOpen} onRequestClose={() => setTaskModalIsOpen(false)} task={selectedTask} />
      )}
    </div>
  );
}
