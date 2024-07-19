import React, { useEffect, useState } from 'react';
import "../styles/homeContent.css";
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';

export default function HomeContent() {
  const [projects, setProjects] = useState([]);
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
      const response = await axios.get('http://localhost:5170/api/Project/GetProjects/', {
        headers: {
          'Authorization': `Bearer ${token}`,
          'Accept': 'application/json'
        }
      });
      setProjects(response.data); 
      
      const responseTasks = await axios.get('http://localhost:5170/api/Project/GetUsersProjectTasks/', {
        headers: {
          'Authorization': `Bearer ${token}`,
          'Accept': 'application/json'
        }
      });
      setProjects(response.data); 
    } catch (error) {
      console.error('Ошибка при выполнении запроса', error);
    }
  };

  return (
    <div className='container-home-auth'>
        <div className='item-in-home'>
            <p>My tasks</p>
            <div className='line-block'></div>
            <div className='tasks-items'>
                {tasks.map(project => (
                <Link to={`/projects/${project.id}`} key={project.id}>
                    <ProjectItem project={project} />
                </Link>
                ))}
            </div>
        </div>
        <div className='item-in-home'>
            <p>My projects</p>
            <div className='line-block'></div>
            <div className='projects-items'>
                {projects.map(project => (
                <Link to={`/projects/${project.id}`} key={project.id}>
                    <ProjectItem project={project} />
                </Link>
                ))}
            </div>
        </div>
    </div>
  );
}
