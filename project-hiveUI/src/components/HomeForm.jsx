import React, { useEffect, useState } from 'react';
import Navbar from './Navbar';
import Menu from './Menu';
import "../styles/home.css";
import Greeting from './Greeting';
import HomeContent from './HomeContent';

export default function HomeForm() {
  const [isUserLoggedIn, setIsUserLoggedIn] = useState(false);

  useEffect(() => {
    handleUserLogined();
  }, []);

  const handleUserLogined = () => {
    const token = localStorage.getItem('jwtToken');
    if (token) {
      setIsUserLoggedIn(true);
    } else {
      setIsUserLoggedIn(false);
    }
  };

  return (
    <div className='container-home'>
      <Navbar onLogout={handleUserLogined} />
      <div className='title-header'>
        <Menu />
        < div className="title-greeting">
          <Greeting />
          {isUserLoggedIn ? <HomeContent /> : <p className='text-description'>Log in to unlock all the features</p>}
        </div>
      </div>
    </div>
  );
}
