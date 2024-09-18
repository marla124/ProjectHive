import React, { useState } from 'react';
import Modal from 'react-modal';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { TextField, Autocomplete } from '@mui/material';
import useFriends from '../hooks/useFriends';
import "../styles/createProjectWindow.css";

export default function CreateProjectForm({ isOpen, onRequestClose }) {
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const token = localStorage.getItem('jwtToken');
  const [users, setUsers] = useState([]);
  const [newUsers, setNewUsers] = useState('');
  const friends = useFriends();
  const navigate = useNavigate();


  const handleCreateProject = async () => {
    const userIds = users.map(user => (user.id));
    try {
      const response = await axios.post(process.env.REACT_APP_API_BASE_URL_PROJECT + '/Project/CreateProject', {
        name,
        description,
        users: userIds
      }, {
        headers: {
          'Authorization': `Bearer ${token}`,
          'Accept': 'application/json',
        },
      });
      console.log('Проект успешно создан!', response.data);
      navigate('/projects');
    } catch (error) {
      console.error('Ошибка при выполнении запроса', error);
      alert('An error occurred while creating the project. Please try again.');
    }
  };

  const handleAddUsers = () => {
    const emails = newUsers.split(',').map(email => email.trim());
    const uniqueEmails = emails.filter(email => email && !users.some(user => user.email === email));
    setUsers([...users, ...uniqueEmails.map(email => ({ email }))]);
    setNewUsers('');
  };

  return (
    <div className="create-project-window">
      <Modal isOpen={isOpen} onRequestClose={onRequestClose} className="react-modal-create">
        <h2 className='title-text-create-project'>Create project</h2>
        <input type="text" className='name-form' placeholder="Enter the name of the project" value={name} onChange={(e) => setName(e.target.value)} required />
        <textarea type="text" className='description-form' placeholder="Enter the description of the project" value={description} onChange={(e) => setDescription(e.target.value)} required />
        <div className="participants-section">
          <Autocomplete
            multiple
            options={friends}
            getOptionLabel={(option) => option.email}
            value={users}
            onChange={(event, newValue) => setUsers(newValue)}
            renderInput={(params) => (
              <TextField
                {...params}
                variant="outlined"
                label="Enter users email"
                placeholder="Enter users email"
                value={newUsers}
                onChange={(e) => setNewUsers(e.target.value)}
                onBlur={handleAddUsers}
              />
            )}
          />
        </div>
        <button className="button-form" onClick={handleCreateProject}>Create</button>
      </Modal>
    </div>
  );
}
