import { useState, useEffect } from 'react';
import useProject from '../hooks/useProject';
import useProjectStatus from '../hooks/useProjectStatus';

export default function useProjectsWithStatus() {
    const [projectsWithStatus, setProjectsWithStatus] = useState([]);
    const projects = useProject([]);
    const statusProjects = useProjectStatus([]);

    useEffect(() => {

        const fetchProjectsWithStatus = () => {
            const withStatus = projects.map(project => {
                const status = statusProjects.find(status => status.id === project.statusProjectId);
                return {
                    ...project,
                    statusName: status ? status.name : 'Unknown'
                };
            });
            setProjectsWithStatus(withStatus);
        };

        fetchProjectsWithStatus();
    }, [projects, statusProjects]);

    return projectsWithStatus;
}
