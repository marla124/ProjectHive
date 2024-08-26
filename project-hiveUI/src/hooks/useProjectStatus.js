import { useState, useEffect } from 'react';
import axios from 'axios';

export default function useProjectStatus() {
    const [statusProjects, setStatusProjects] = useState([]);
    const token = localStorage.getItem('jwtToken');

    useEffect(() => {
        const fetchStatusProjects = async () => {
            try {
                const response = await axios.get('http://localhost:5170/api/Project/GetStatusProject', {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Accept': 'application/json'
                    }
                });
                console.log('Status Projects response:', response.data);
                setStatusProjects(response.data);
            } catch (error) {
                console.error('Ошибка при выполнении запроса', error);
            }
        };
        fetchStatusProjects();
    }, [])

    return statusProjects;
}
