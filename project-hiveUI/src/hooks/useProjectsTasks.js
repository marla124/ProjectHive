import { useState, useEffect } from 'react';
import axios from 'axios';

export default function useProjectsTasks() {
    const token = localStorage.getItem('jwtToken');
    const [projectsTasks, setProjectsTasks] = useState([]);
    const url = window.location.href;
    const projectId = url.split('/').pop();
    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get(`http://localhost:5170/api/ProjectTask/GetProjectTasks/${projectId}`, {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Accept': 'application/json'
                    }
                });
                const sortedProjectTasks = response.data.sort((a, b) => new Date(b.startExecution) - new Date(a.createdDate));
                setProjectsTasks(sortedProjectTasks);
            } catch (error) {
                console.error('Ошибка при выполнении запроса', error);
            }
        };

        fetchData();
    }, [])

    return projectsTasks;
}
