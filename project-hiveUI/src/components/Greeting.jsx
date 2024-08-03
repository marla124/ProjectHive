import React from 'react';
import "../styles/greeting.css"

export default function Greeting() {
  const now = new Date();
  const hour = now.getHours();

  let greeting;
  if (hour >= 6 && hour < 12) {
    greeting = 'Good morning!';
  } else if (hour >= 12 && hour < 18) {
    greeting = 'Good afternoon!';
  } else {
    greeting = 'Good evening!';
  }

  const options = { weekday: 'long', month: 'long', day: 'numeric' };
  const formattedDate = now.toLocaleDateString('en-US', options);

  return (
    <div className='greeting'>
      <p className='time-of-day'>{greeting}</p>
      <p className='date'>{formattedDate}</p>
    </div>
  );
}
