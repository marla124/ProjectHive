import { useState, useEffect } from 'react';
import axios from 'axios';

export default function useProject() {
    const [projects, setProjects] = useState([]);
    const token = localStorage.getItem('jwtToken');

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get('http://localhost:5170/api/Project/GetProjects', {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Accept': 'application/json'
                    }
                });
                const sortedProjects = response.data.sort((a, b) => new Date(b.createdAt) - new Date(a.createdDate));
                const latestProjects = sortedProjects.slice(0, 5);
                setProjects(latestProjects);
            } catch (error) {
                console.error('Ошибка при выполнении запроса', error);
            }
        };

        fetchData();
    }, [])

    return projects;
}
