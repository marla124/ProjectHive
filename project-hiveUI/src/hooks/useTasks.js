import { useState, useEffect } from 'react';
import axios from 'axios';

export default function useTasks() {
    const [tasks, setProjectTasks] = useState([]);
    const token = localStorage.getItem('jwtToken');

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get(process.env.REACT_APP_API_BASE_URL_PROJECT + `/ProjectTask/GetProjectTasksForUser`, {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Accept': 'application/json'
                    }
                });
                const sortedProjectTasks = response.data.sort((a, b) => new Date(b.startExecution) - new Date(a.createdDate));

                const responseUser = await axios.get(process.env.REACT_APP_API_BASE_URL_USER + `/User/GetUsers`, {
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