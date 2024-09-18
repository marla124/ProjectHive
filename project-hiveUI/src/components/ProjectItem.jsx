import React from 'react';
import "../styles/projectItem.css"

export default function ProjectItem(props) {

  return (
    <div className='project-item'>
      <p className='name-project'>{props.project.name}</p>
      <p className='status-project'>{props.project.statusName}</p>
    </div>
  );
}