import React from 'react';
import Modal from 'react-modal';
import { formatDate } from "../utils/formatDate"
import "../styles/taskForm.css"

export default function TaskForm({ isOpen, onRequestClose, task }) {
    return (
        <Modal isOpen={isOpen} onRequestClose={onRequestClose} className="model-open">
            <form >
                <p className='title-task-name'>{task.name} </p>
                <p className='descript-task'>{task.description}</p>
                <div className='bot-part'>
                    <p className='status-usertask'>{task.statusName}</p>
                    <p className='user-exx'>{task.userExecutorName}</p>
                </div>
                <div className='deadline-task'>
                    <i className="icon-calendar" aria-hidden="true"></i>
                    <p className='deadline-text'>{formatDate(task.deadline)}</p>
                </div>
            </form>
        </Modal>
    );
}