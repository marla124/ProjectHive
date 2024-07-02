import React, { useState } from 'react';
import Navbar from './Navbar';
import Menu from './Menu';
import "../styles/workPlace.css"
import TaskItem from './TaskItem';

export default function WorkPlacePage(){
    const [tasks, setTasks] = useState([
        { id: 1, name: 'Task1', status: "In Process", description: "Description1", deadline: "12.07.2024" },
        { id: 2, name: 'Task2', status: "Done", description: "Description2useeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeei", deadline: "12.07.2024"  },
        { id: 3, name: 'Task3', status: "Cancelled", description: "Description3", deadline: "12.07.2024"  },
        { id: 4, name: 'Task4', status: "Open", description: "Dessjhdjshfkjaoihdisuhdiuhuihudhhhhhhhhhhhhhhhhhhiuddddhidhdhudhishdihaishaidiaudshaidhaihdjhsjfhsjhdjon4", deadline: "12.07.2024"  },
        { id: 5, name: 'Task5', status: "Open", description: "Description5ijoisudfiosudisuhaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabilzzzzzzzzzzzzzzzzzzzzzfdsvbhyklFFFFFFSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSG", deadline: "12.07.2024"  },
        { id: 6, name: 'Task6', status: "In Process", description: "Description6", deadline: "12.07.2024"  },
        { id: 7, name: 'Task6', status: "In Process", description: "Description6", deadline: "12.07.2024"  },
        { id: 8, name: 'Task6', status: "In Process", description: "Description6", deadline: "12.07.2024"  },
        { id: 9, name: 'Task6', status: "In Process", description: "Description6", deadline: "12.07.2024"  },
        { id: 10, name: 'Task6', status: "In Process", description: "Description6", deadline: "12.07.2024"  },
        { id: 11, name: 'Task6', status: "In Process", description: "Description6", deadline: "12.07.2024"  },
        { id: 12, name: 'Task6', status: "In Process", description: "Description6", deadline: "12.07.2024"  },
        { id: 13, name: 'Task6', status: "In Process", description: "Description6", deadline: "12.07.2024"  },
        { id: 14, name: 'Task6', status: "In Process", description: "Description6", deadline: "12.07.2024"  },
        { id: 15, name: 'Task6', status: "In Process", description: "Description6", deadline: "12.07.2024"  },
        { id: 16, name: 'Task6', status: "In Process", description: "Description6", deadline: "12.07.2024"  },
        { id: 17, name: 'Task6', status: "In Process", description: "Description6", deadline: "12.07.2024"  },
        { id: 18, name: 'Task6', status: "In Process", description: "Description6", deadline: "12.07.2024"  },
        { id: 19, name: 'Task6', status: "In Process", description: "Description6", deadline: "12.07.2024"  },
        { id: 20, name: 'Task6', status: "In Process", description: "Description6", deadline: "12.07.2024"  },
    ]);
    const statusValues = ['Open', 'Done', 'In Process', 'Cancelled'];
    return(
    <div className='page-container'>
        <Navbar />
        <div className='tasks-container'>
            <Menu />
            <div className='tasks-list'>
            {statusValues.map(status => (
            <div key={status} className='column'>
                <h2 className='column-header'>{status}</h2>
                <div className='column-body'>
                {tasks.filter(task => task.status === status).map(filteredTask => (
                    <TaskItem task={filteredTask} key={filteredTask.id} />
                ))}
                </div>
            </div>
        ))}
    </div>
        </div>  
    </div>
    );
}