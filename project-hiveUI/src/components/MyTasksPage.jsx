import React, { useState, useEffect } from 'react';
import Navbar from './Navbar';
import Menu from './Menu';
import "../styles/mytask.css"
import axios from 'axios';
import TaskItem from './TaskItem';
import { Link } from 'react-router-dom';

export default function MyTasksPage() {
  const [tasks, setProjectTasks] = useState([]);
  const [statusTask, setStatusTask] = useState([]);
  const token = localStorage.getItem('jwtToken');

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const response = await axios.get(`http://localhost:5170/api/ProjectTask/GetProjectTasksForUser`, {
        headers: {
          'Authorization': `Bearer ${token}`,
          'Accept': 'application/json'
        }
      });
      setProjectTasks(response.data);
      const responseStatus = await axios.get('http://localhost:5170/api/ProjectTask/GetStatusProjectTasks', {
        headers: {
          'Authorization': `Bearer ${token}`,
          'Accept': 'application/json'
        }
      });
      setStatusTask(responseStatus.data);

      const taskWithStatus = response.data.map(task => {
        const status = responseStatus.data.find(status => status.id === task.statusTaskId);
        return {
          ...task,
          statusName: status.name
        };
      });
      setProjectTasks(taskWithStatus);
    } catch (error) {
      console.error('Ошибка при выполнении запроса', error);
    }
  };
  return (
    <div className='page-mytasks-container'>
      <Navbar />
      <div className='mytasks-container'>
        <Menu />
        <div className='list-mytasks'>
          <h1 className='title-header-mytasks'>My Tasks</h1>
          <div className='mytask'>
            {tasks.map(task => (
              <Link to={`/mytasks/${task.id}`} key={task.id}>
                <TaskItem task={task} className="tasl-item" />
              </Link>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}