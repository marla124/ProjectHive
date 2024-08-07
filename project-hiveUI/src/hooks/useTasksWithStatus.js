import { useState, useEffect } from 'react';
import useTasks from '../hooks/useTasks';
import useStatusTasks from '../hooks/useStatusTasks';

export default function useTasksWithStatus(token) {
    const [tasksWithStatus, setTasksWithStatus] = useState([]);
    const tasks = useTasks(token);
    const statusTasks = useStatusTasks(token);

    useEffect(() => {
        const fetchData = async () => {
            const withStatus = tasks.map(task => {
                const status = statusTasks.find(status => status.id === task.statusTaskId);
                return {
                    ...task,
                    statusName: status ? status.name : 'Unknown'
                };
            });
            setTasksWithStatus(withStatus);
        };

        fetchData();
    }, [tasks, statusTasks]);

    return tasksWithStatus;
}
