import { useState, useEffect } from 'react';
import axios from 'axios';

export default function useStatusTasks() {
    const [statusTask, setStatusTask] = useState([]);
    const token = localStorage.getItem('jwtToken');

    useEffect(() => {
        const fetchData = async () => {
            try {
                const responseStatus = await axios.get(process.env.REACT_APP_API_BASE_URL_PROJECT + '/ProjectTask/GetStatusProjectTasks', {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Accept': 'application/json'
                    }
                });
                setStatusTask(responseStatus.data);
            } catch (error) {
                console.error('Ошибка при выполнении запроса', error);
            }
        };

        fetchData();
    }, [token])

    return statusTask;
}