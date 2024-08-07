import React, { useState } from 'react';
import Navbar from './Navbar';
import Menu from './Menu';
import useProjectsTasks from '../hooks/useProjectsTasks';
import useStatusTasks from '../hooks/useStatusTasks';
import { useNavigate } from 'react-router-dom';
import "../styles/workPlace.css";
import TaskItem from './TaskItem';
import useUserAuthentication from '../hooks/useUserAuthentication';
import CreateTaskForm from './CreateTaskForm';

export default function WorkPlacePage() {
  const statusTasks = useStatusTasks();
  const navigate = useNavigate();
  const projectsTasks = useProjectsTasks();
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const isUserLoggedIn = useUserAuthentication(false);

  const handleTaskButtonClick = () => {
    if (isUserLoggedIn) {
      setModalIsOpen(true);
    } else {
      navigate('/login');
    }
  };
  return (
    <div className='page-container'>
      <Navbar />
      <div className='tasks-container'>
        <Menu />
        <div className='tasks-list'>
          {statusTasks.map(status => (
            <div key={status.id} className='column'>
              <h2 className='column-header'>{status.name}</h2>
              <div className='column-body'>
                {status.name === 'Open' && (
                  <div>
                    <button className='more-item-place' onClick={handleTaskButtonClick}>
                      <i className="icon-plus" aria-hidden="true"></i>
                    </button>
                    <CreateTaskForm isOpen={modalIsOpen} onRequestClose={() => setModalIsOpen(false)} />
                  </div>
                )}
                {projectsTasks.filter(task => task.statusTaskId === status.id).map(filteredTask => (
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
