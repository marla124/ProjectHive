import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import Navbar from './Navbar';
import Menu from './Menu';
import "../styles/workPlace.css"
import TaskItem from './TaskItem';

export default function WorkPlacePage(){
    const [tasks, setProjectTasks] = useState([]);
    const [statusTask, setStatusTask] = useState([]);
    const navigate = useNavigate();
    const token = localStorage.getItem('jwtToken');
    const url=window.location.href;
    const projectId = url.split('/').pop();

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
        const response = await axios.get(`http://localhost:5170/api/ProjectTask/GetProjectTasks/${projectId}`, {
          headers: {
            'Authorization': `Bearer ${token}`,
            'Accept': 'application/json'
          }
        });
        console.log('Response data:', response.data);
        setProjectTasks(response.data);        

        const responseStatus = await axios.get('http://localhost:5170/api/ProjectTask/GetStatusProjectTasks', {
          headers: {
            'Authorization': `Bearer ${token}`,
            'Accept': 'application/json'
          }
        });
        console.log('Response data:', responseStatus.data);
        setStatusTask(responseStatus.data); 

      } catch (error) {
        console.error('Ошибка при выполнении запроса', error);
      }
    };

    return(
    <div className='page-container'>
        <Navbar />
        <div className='tasks-container'>
            <Menu />
            <div className='tasks-list'>
            {statusTask.map(status => (
            <div key={status.id} className='column'>
                <h2 className='column-header'>{status.name}</h2>
                <div className='column-body'>
                {tasks.filter(task => task.statusTaskId === status.id).map(filteredTask => (
                    <TaskItem task={filteredTask} key={filteredTask.id} />
                ))}
                </div>
            </div>
        ))}
    </div>
        </div>  
    </div>
    );
}