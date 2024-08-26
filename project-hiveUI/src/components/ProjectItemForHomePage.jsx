import React from 'react';
import "../styles/projectItemForPage.css"

export default function ProjectItemForHomePage(props) {

  return (
    <div className='project-item-for-home-page'>
      <p className='name-project-for-home-page'>{props.project.name}</p>
      <p className='status-project-for-home-page'>{props.project.statusName}</p>
    </div>
  );
}