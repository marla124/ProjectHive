import React, { useState, useEffect } from 'react';
import Modal from 'react-modal';
import axios from 'axios';
import useStatusTask from '../hooks/useStatusTasks';
import DatePicker from 'react-datepicker';
import "../styles/createTask.css";
import 'react-datepicker/dist/react-datepicker.css';
import "../styles/createProjectWindow.css";

export default function CreateProjectForm({ isOpen, onRequestClose }) {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [deadline, setDeadline] = useState(null);
    const url = window.location.href;
    const projectId = url.split('/').pop();
    const [statusTaskId, setStatusTaskId] = useState('');
    const token = localStorage.getItem('jwtToken');
    const statuses = useStatusTask();

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
                statusTaskId
            }, {
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Accept': 'application/json',
                },
            });
            alert('Проект успешно создан!');
        } catch (error) {
            console.error('Ошибка при выполнении запроса', error);
            alert('Произошла ошибка при создании задачи. Пожалуйста, попробуйте снова.');
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
                <button className="button-form" onClick={handleCreateProject}>Create</button>
            </Modal>
        </div>
    );
}
