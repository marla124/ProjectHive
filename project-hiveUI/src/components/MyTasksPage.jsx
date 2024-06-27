import React, { useState } from 'react';
import Navbar from './Navbar';
import Menu from './Menu';
import "../styles/mytask.css"
import TaskItem from './TaskItem';
import { Link} from 'react-router-dom';

export default function MyTasksPage(){
    const [tasks, setTasks] = useState([
        { id: 1, name: 'Task1', status: "In Process", description: "Description1", deadline: "12.07.2024" },
        { id: 2, name: 'Task2', status: "Done", description: "Description2useeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeei", deadline: "12.07.2024"  },
        { id: 3, name: 'Task3', status: "Cancelled", description: "Description3", deadline: "12.07.2024"  },
        { id: 4, name: 'Task4', status: "Open", description: "Dessjhdjshfkjaoihdisuhdiuhuihudhhhhhhhhhhhhhhhhhhiuddddhidhdhudhishdihaishaidiaudshaidhaihdjhsjfhsjhdjon4", deadline: "12.07.2024"  },
        { id: 5, name: 'Task5', status: "Open", description: "Description5ijoisudfiosudisuhaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabilzzzzzzzzzzzzzzzzzzzzzfdsvbhyklFFFFFFSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSG", deadline: "12.07.2024"  },
        { id: 6, name: 'Task6', status: "In Process", description: "Description6", deadline: "12.07.2024"  },
        ]);

    return(
    <div className='page-mytasks-container'>
        <Navbar />
        <div className='mytasks-container'>
        <Menu />
        <div className='list-mytasks'>
            <h1 className='title-header-mytasks'>My Tasks</h1>
            <div className='mytask'>
                {tasks.map(task => (
                <Link to='/mytask' key={tasks.id}>
                    <TaskItem tasks={tasks} />
                </Link>
                ))}
            </div>
        </div>
    </div>  
    </div>
    );
}