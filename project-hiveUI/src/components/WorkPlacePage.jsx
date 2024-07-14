import React, { useState } from 'react';
import Navbar from './Navbar';
import Menu from './Menu';
import "../styles/workPlace.css"
import TaskItem from './TaskItem';

export default function WorkPlacePage(){
    const [tasks, setTasks] = useState([]);
    const statusValues = ['Open', 'Done', 'In Process', 'Cancelled'];
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
        const response = await axios.get('http://localhost:5170/api/ProjectTask/GetProjectTasks', {
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
    <div className='page-container'>
        <Navbar />
        <div className='tasks-container'>
            <Menu />
            <div className='tasks-list'>
            {statusValues.map(status => (
            <div key={status} className='column'>
                <h2 className='column-header'>{status}</h2>
                <div className='column-body'>
                {tasks.filter(task => task.status === status).map(filteredTask => (
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