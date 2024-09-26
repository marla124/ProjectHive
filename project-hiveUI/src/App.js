import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import LoginForm from './components/LoginForm.jsx'
import RegistrationForm from './components/RegistrationForm.jsx'
import HomeForm from './components/HomeForm.jsx'
import NotificationForm from './components/NotificationForm.jsx'
import ProjectsListPage from './components/ProjectsListPage.jsx';
import WorkPlacePage from './components/WorkPlacePage.jsx';
import MyTasksPage from './components/MyTasksPage.jsx';
import axios from 'axios';

axios.interceptors.response.use(
  response => response,
  async error => {
    const firstRequest = error.config;
    if (error.response.status == 401 && !firstRequest._retry) {
      firstRequest._retry = true;
      await refreshToken();
      firstRequest.headers['Authorization'] = `Bearer ${localStorage.getItem('jwtToken')}`;
      return axios(firstRequest);
    }
    return Promise.reject(error);
  }
);
const refreshToken = async () => {
  try {
    const storedRefreshToken = localStorage.getItem('refreshToken');

    const response = await axios.post('/api/Token/GenerateTokenByRefresh', {
      refreshToken: storedRefreshToken
    }, {
      headers: {
        'Content-Type': 'application/json'
      }
    });

    const { jwtToken, refreshToken } = response.data;
    localStorage.setItem('jwtToken', jwtToken);
    localStorage.setItem('refreshToken', refreshToken);

    return jwtToken;
  } catch (error) {
    console.error('Error updating token', error);
  }
};
const PrivateRoute = ({ element: Component, ...rest }) => {
  const token = localStorage.getItem('jwtToken');
  return token ? <Component {...rest} /> : <Navigate to="/login" />;
};


function App() {
  return (
    <>
      <Routes>
        <Route path="/" element={<HomeForm />} />
        <Route path="/projects" element={<PrivateRoute element={ProjectsListPage} />} />
        <Route path="/projects/:id" element={<PrivateRoute element={WorkPlacePage} />} />
        <Route path="/mytasks" element={<PrivateRoute element={MyTasksPage} />} />
        <Route path="/login" element={<LoginForm />} />
        <Route path="/signup" element={<RegistrationForm />} />
        <Route path="/notifications" element={<PrivateRoute element={NotificationForm} />} />
      </Routes></>
  );
}

export default App;
