import React, { useState, useEffect } from 'react';
import Navbar from './Navbar';
import Menu from './Menu';
import "../styles/mytask.css"
import axios from 'axios';
import TaskItem from './TaskItem';
import { Link, useNavigate} from 'react-router-dom';

export default function MyTasksPage(){
    const [tasks, setProjectTasks] = useState([]);
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
        const response = await axios.get(`http://localhost:5170/api/ProjectTask/GetProjectTasksForUser`, {
          headers: {
            'Authorization': `Bearer ${token}`,
            'Accept': 'application/json'
          }
        });
        setProjectTasks(response.data); 
      } catch (error) {
        console.error('Ошибка при выполнении запроса', error);
      }
    };
    return(
    <div className='page-mytasks-container'>
        <Navbar />
        <div className='mytasks-container'>
        <Menu />
        <div className='list-mytasks'>
            <h1 className='title-header-mytasks'>My Tasks</h1>
            <div className='mytask'>
            {tasks.map(task => (
                <Link to={`/mytasks/${task.id}`} key={task.id}>
                    <TaskItem task={task} className="tasl-item"/>
                </Link>
                ))}
            </div>
        </div>
    </div>  
    </div>
    );
}