import React from 'react';
import Navbar from './Navbar';
import Menu from './Menu';
import "../styles/home.css";
import Greeting from './Greeting';
import HomeContent from './HomeContent';

export default function HomeForm() {
  return (
    <div className='container-home'>
      <Navbar />
      <div className='title-header'>
        <Menu />
        < div className="title-greeting">
          <Greeting/>
          <HomeContent />
          <p className='text-description'>Log in to unlock all the features</p>
        </div>
      </div>
    </div>
  );
}
