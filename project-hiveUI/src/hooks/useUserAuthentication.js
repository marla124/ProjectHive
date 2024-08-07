import { useState, useEffect } from 'react';

const useUserAuthentication = () => {
    const [isUserLoggedIn, setIsUserLoggedIn] = useState(false);

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (token) {
            setIsUserLoggedIn(true);
        }
    }, []);

    return { isUserLoggedIn };
};

export default useUserAuthentication;
