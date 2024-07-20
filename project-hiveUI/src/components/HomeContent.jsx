import React, { useEffect, useState } from 'react';
import "../styles/homeContent.css";
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';
import ProjectItemForHomePage from './ProjectItemForHomePage';
import TaskItemForHomePage from './TaskItemForHomePage';
import CreateProjectForm from './CreateProjectForm';

export default function HomeContent() {
  const [projects, setProjects] = useState([]);
  const [tasks, setProjectTasks] = useState([]);
    const [modalIsOpen, setModalIsOpen] = useState(false);
    const navigate = useNavigate();
  const token = localStorage.getItem('jwtToken');
  useEffect(() => {
    if (!token) {
      navigate('/login');
    } else {
      fetchData();
    }
  }, [token, navigate]);

  const fetchData = async () => {
    console.log(token);

    if (!token) {
      console.error('Токен не найден');
      return;
    }

    try {
      const response = await axios.get('http://localhost:5170/api/Project/GetProjects', {
        headers: {
          'Authorization': `Bearer ${token}`,
          'Accept': 'application/json'
        }
      });
      const sortedProjects = response.data.sort((a, b) => new Date(b.createdAt) - new Date(a.createdDate));
      const latestProjects = sortedProjects.slice(0, 5);
      setProjects(latestProjects);
      
      const responseTasks = await axios.get('http://localhost:5170/api/Project/GetProjectTasksForUser', {
        headers: {
          'Authorization': `Bearer ${token}`,
          'Accept': 'application/json'
        }
      });
      setProjectTasks(responseTasks.data); 
    } catch (error) {
      console.error('Ошибка при выполнении запроса', error);
    }
  };

  return (
    <div className='container-home-auth'>
        <div className='item-in-home'>
            <p>My tasks</p>
            <div className='line-block'></div>
            <div className='mytasks-items'>
                {tasks.map(task => (
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
                {projects.map(project => (
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
