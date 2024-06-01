import React from 'react';
import { Routes, Route } from 'react-router-dom';
import LoginForm from './components/LoginForm.jsx'
import RegistrationForm from './components/RegistrationForm.jsx'
import HomeForm from './components/HomeForm.jsx'


function App() {
  return (
      <>
      <Routes>
        <Route path="/" element={<HomeForm />} />
        <Route path="/login" element={<LoginForm />} />
        <Route path="/signup" element={<RegistrationForm />} />
      </Routes></>
  );
}

export default App;
