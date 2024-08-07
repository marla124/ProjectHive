import React from 'react';
import Navbar from './Navbar';
import Menu from './Menu';
import "../styles/mytask.css"
import TaskItem from './TaskItem';
import { Link } from 'react-router-dom';
import useTasksWithStatus from '../hooks/useTasksWithStatus';

export default function MyTasksPage() {
  const tasksWithStatus = useTasksWithStatus();

  return (
    <div className='page-mytasks-container'>
      <Navbar />
      <div className='mytasks-container'>
        <Menu />
        <div className='list-mytasks'>
          <h1 className='title-header-mytasks'>My Tasks</h1>
          <div className='mytask'>
            {tasksWithStatus.map(task => (
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