import { useState, useEffect } from 'react';
import axios from 'axios';

export default function useTasks() {
    const [tasks, setProjectTasks] = useState([]);
    const token = localStorage.getItem('jwtToken');

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get(`http://localhost:5170/api/ProjectTask/GetProjectTasksForUser`, {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Accept': 'application/json'
                    }
                });
                const sortedProjectTasks = response.data.sort((a, b) => new Date(b.startExecution) - new Date(a.createdDate));

                const responseUser = await axios.get(`http://localhost:5183/api/User/GetUsers`, {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Accept': 'application/json'
                    }
                });
                const users = responseUser.data;

                const tasksWithUserNames = sortedProjectTasks.map(task => {
                    const user = users.find(user => user.id === task.userExecutorId);
                    return {
                        ...task,
                        userExecutorName: user ? user.email : 'Unknown'
                    };
                });

                setProjectTasks(tasksWithUserNames);
            } catch (error) {
                console.error('Ошибка при выполнении запроса', error);
            }
        };

        fetchData();
    }, [token])

    return tasks;
}