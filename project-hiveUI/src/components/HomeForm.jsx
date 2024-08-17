import React from 'react';
import Navbar from './Navbar';
import Menu from './Menu';
import "../styles/home.css";
import Greeting from './Greeting';
import HomeContent from './HomeContent';
import useUserAuthentication from '../hooks/useUserAuthentication';

export default function HomeForm() {
  const { isUserLoggedIn } = useUserAuthentication();

  return (
    <div className='container-home'>
      <Navbar />
      <div className='title-header'>
        <Menu />
        <div className="title-greeting">
          <Greeting />
          {isUserLoggedIn ? <HomeContent /> : <p className='text-description'>Log in to unlock all the features</p>}
        </div>
      </div>
    </div>
  );
}
