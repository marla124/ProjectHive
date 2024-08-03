import React from 'react';
import "../styles/taskItemForHomePage.css"

export default function TaskItemForHomePage(props) {

    return (
        <div className='task-item-for-home-page'>
            <p className='name-task-for-home-page'>{props.task.name}</p>
            <p className='status-task-for-home-page'>{props.task.statusName}</p>
        </div>
    );
}
