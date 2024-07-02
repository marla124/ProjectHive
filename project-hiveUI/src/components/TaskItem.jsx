import React from 'react';
import "../styles/taskItem.css"

export default function TaskItem(props){

    return(
      <div className='task-item'>
        <div className='p-container'>
        <p className='name-task'>{props.task.name}</p>
        <p className='description-task'>{props.task.description}</p>
        <div className='deadline-task'>
            <i className="icon-calendar" aria-hidden="true"></i>
            <p className='deadline-text'>{props.task.deadline}</p>
        </div>
      </div>
      </div>
    );
}