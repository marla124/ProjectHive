import React, { useState, useEffect } from 'react';
import Modal from 'react-modal';
import axios from 'axios';
import useStatusTask from '../hooks/useStatusTasks';
import useFriends from '../hooks/useFriends'; import TextField from '@mui/material/TextField';
import Autocomplete from '@mui/lab/Autocomplete';
import DatePicker from 'react-datepicker';
import "../styles/createTask.css";
import 'react-datepicker/dist/react-datepicker.css';
import "../styles/createProjectWindow.css";

export default function CreateProjectForm({ isOpen, onRequestClose }) {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [deadline, setDeadline] = useState(null);
    const [userExecutorId, setUserExecutorId] = useState(null);
    const url = window.location.href;
    const projectId = url.split('/').pop();
    const [statusTaskId, setStatusTaskId] = useState('');
    const token = localStorage.getItem('jwtToken');
    const statuses = useStatusTask();
    const friends = useFriends();

    useEffect(() => {
        const openStatus = statuses.find(status => status.name === 'Open');
        if (openStatus) {
            setStatusTaskId(openStatus.id);
        }
    }, [statuses]);


    const handleCreateProject = async () => {
        try {
            await axios.post('http://localhost:5170/api/ProjectTask/CreateTask', {
                name,
                description,
                deadline: deadline ? deadline.toISOString() : null,
                projectId,
                statusTaskId,
                userExecutorId
            }, {
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Accept': 'application/json',
                },
            });
            alert('The task has been successfully created!');
            window.location.reload();
        } catch (error) {
            console.error('Ошибка при выполнении запроса', error);
            alert('An error occurred while creating the task. Please try again.');
        }
    };

    return (
        <div className="create-project-window">
            <Modal isOpen={isOpen} onRequestClose={onRequestClose} className="react-modal">
                <h2 className='title-text-create-project'>Create task</h2>
                <input type="text" className='name-form' placeholder="Enter the name of the task" value={name} onChange={(e) => setName(e.target.value)} required />
                <textarea type="text" className='description-form' placeholder="Enter the description of the task" value={description} onChange={(e) => setDescription(e.target.value)} required />
                <DatePicker selected={deadline} onChange={(date) => setDeadline(date)} dateFormat="yyyy/MM/dd" className='deadline-picker' placeholderText="Select a deadline" />
                <select className='status-select' value={statusTaskId} onChange={(e) => {
                    console.log('Selected status ID:', e.target.value);
                    setStatusTaskId(e.target.value);
                }} required>
                    {statuses.map((status) => (
                        <option key={status.id} value={status.id}>{status.name}</option>
                    ))}
                </select>
                <Autocomplete
                    className='autocomplete-task'
                    freeSolo
                    autoComplete
                    autoHighlight
                    options={friends}
                    getOptionLabel={(option) => option.name}
                    onChange={(event, newValue) => setUserExecutorId(newValue)}
                    renderInput={(params) => (
                        <TextField
                            {...params}
                            variant="outlined"
                            label="Enter the performer's user"
                        />
                    )}
                />
                <button className="button-form" onClick={handleCreateProject}>Create</button>
            </Modal>
        </div>
    );
}
