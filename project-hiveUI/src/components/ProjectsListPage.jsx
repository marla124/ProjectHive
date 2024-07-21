import React, { useEffect, useState } from 'react';
import Navbar from './Navbar';
import Menu from './Menu';
import "../styles/projects.css";
import ProjectItem from './ProjectItem';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';

export default function ProjectsListPage() {
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
      setProjects(response.data); 
    } catch (error) {
      console.error('Ошибка при выполнении запроса', error);
    }
  };

  return (
    <div className='page-container'>
      <Navbar />
      <div className='projects-container'>
        <Menu />
        <div className='projects-list'>
          <h1 className='title-header'>My Projects</h1>
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
