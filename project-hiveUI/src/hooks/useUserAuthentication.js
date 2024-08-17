import { useState, useEffect } from 'react';

const useUserAuthentication = () => {
    const [isUserLoggedIn, setIsUserLoggedIn] = useState(false);

    useEffect(() => {
        const token = localStorage.getItem('jwtToken');
        setIsUserLoggedIn(!!token);
    }, []);

    return { isUserLoggedIn };
};

export default useUserAuthentication;
