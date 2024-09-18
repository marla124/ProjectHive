import { useState, useEffect } from 'react';
import axios from 'axios';

export default function useProjectStatus() {
    const [statusProjects, setStatusProjects] = useState([]);
    const token = localStorage.getItem('jwtToken');

    useEffect(() => {
        const fetchStatusProjects = async () => {
            try {
                const response = await axios.get(process.env.REACT_APP_API_BASE_URL_PROJECT + '/Project/GetStatusProject', {
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
