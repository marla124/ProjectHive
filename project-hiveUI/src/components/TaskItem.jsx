import React from 'react';
import "../styles/taskItem.css"
import { formatDate } from "../utils/formatDate"

export default function TaskItem(props) {
  return (
    <div className='task-item'>
      <div className='p-container'>
        <p className='name-task'>{props.task.name}</p>
        <div className='bottom-part-task'>
          {props.task.statusName ? <p className='status-task'>{props.task.statusName}</p> : <p className='unk'></p>}
          <div className='deadline-task'>
            <i className="icon-calendar" aria-hidden="true"></i>
            <p className='deadline-text'>{formatDate(props.task.deadline)}</p>
          </div>
        </div>
      </div>
    </div>
  );
}