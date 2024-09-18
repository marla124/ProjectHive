import axios from 'axios';

const apiClient = axios.create({
    baseURL: 'http://localhost:5170/api',
    headers: {
        'Authorization': `Bearer ${localStorage.getItem("jwtToken")}`,
        'Accept': 'application/json'
    }
});

export const fetchTasks = () => apiClient.get('/ProjectTask/GetProjectTasksForUser');
export const fetchStatusTasks = () => apiClient.get('/ProjectTask/GetStatusProjectTasks');
