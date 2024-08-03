import React, { useState, useEffect } from 'react';
import Modal from 'react-modal';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import "../styles/createProjectWindow.css";

export default function CreateProjectForm({ isOpen, onRequestClose }) {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const token = localStorage.getItem('jwtToken');
  const navigate = useNavigate();

  const handleCreateProject = async () => {
    try {
      await axios.post('http://localhost:5170/api/Project/CreateProject', {
        name,
        description,
      }, {
        headers: {
          'Authorization': `Bearer ${token}`,
          'Accept': 'application/json',
        },
      });
      console.log('Проект успешно создан!');
      navigate('/projects');
    } catch (error) {
      console.error('Ошибка при выполнении запроса', error);
      alert('Произошла ошибка при создании проекта. Пожалуйста, попробуйте снова.');
    }
  };

  return (
    <div className="create-project-window">
      <Modal isOpen={isOpen} onRequestClose={onRequestClose} className="react-modal">
        <h2 className='title-text-create-project'>Create project</h2>
        <input type="text" className='name-form' placeholder="Enter the name of the project" value={name} onChange={(e) => setName(e.target.value)} required />
        <textarea type="text" className='description-form' placeholder="Enter the description of the project" value={description} onChange={(e) => setDescription(e.target.value)} required />
        <button className="button-form" onClick={handleCreateProject}>Create</button>
      </Modal>
    </div>
  );
}
