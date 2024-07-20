import React from 'react';
import "../styles/taskItemForHomePage.css"

export default function TaskItemForHomePage(props){

    return(
        <div className='task-item-for-home-page'>
            <p className='name-project-for-home-page'>{props.task.name}</p>
        </div>
    );
}
