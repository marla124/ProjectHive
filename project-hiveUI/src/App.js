import React from 'react';
import { Routes, Route } from 'react-router-dom';
import LoginForm from './components/LoginForm.jsx'
import RegistrationForm from './components/RegistrationForm.jsx'
import HomeForm from './components/HomeForm.jsx'
import NotificationForm from './components/NotificationForm.jsx'
import ProjectsListPage from './components/ProjectsListPage.jsx';


function App() {
  return (
      <>
      <Routes>
        <Route path="/" element={<HomeForm />} />
        <Route path="/projects" element={<ProjectsListPage />} />
        <Route path="/login" element={<LoginForm />} />
        <Route path="/signup" element={<RegistrationForm />} />
        <Route path="/notifications" element={<NotificationForm />} />
      </Routes></>
  );
}

export default App;
