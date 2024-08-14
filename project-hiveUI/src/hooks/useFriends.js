import { useState, useEffect } from 'react';
import axios from 'axios';

export default function useFriends() {
    const [friends, setFriends] = useState([]);
    const token = localStorage.getItem('jwtToken');

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get('http://localhost:5183/api/User/GetFriendlyUser', {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                        'Accept': 'application/json'
                    }
                });
                const newFriends = response.data.sort((a, b) => new Date(b.createdAt) - new Date(a.createdDate));
                setFriends(newFriends);
            } catch (error) {
                console.error('Ошибка при выполнении запроса', error);
            }
        };

        fetchData();
    }, [])

    return friends;
}
