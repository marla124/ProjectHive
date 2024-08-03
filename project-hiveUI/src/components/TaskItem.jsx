import React from 'react';
import "../styles/taskItem.css"

export default function TaskItem(props) {
  const formatDate = (dateString) => {
    const options = { year: 'numeric', month: 'long', day: 'numeric' };
    return new Date(dateString).toLocaleDateString('ru-RU', options);
  };
  return (
    <div className='task-item'>
      <div className='p-container'>
        <p className='name-task'>{props.task.name}</p>
        <div className='bottom-part-task'>
          <p className='status-task'>{props.task.statusName}</p>
          <div className='deadline-task'>
            <i className="icon-calendar" aria-hidden="true"></i>
            <p className='deadline-text'>{formatDate(props.task.deadline)}</p>
          </div>
        </div>
      </div>
    </div>
  );
}