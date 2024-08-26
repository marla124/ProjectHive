import React, { useState, useEffect } from 'react';
import Modal from 'react-modal';
import axios from 'axios';
import AddFriendForm from './AddFriendForm';
import { useNavigate } from 'react-router-dom';
import TextField from '@mui/material/TextField';
import Autocomplete from '@mui/lab/Autocomplete';
import "../styles/search.css";

export default function SearchForm({ isOpen, onRequestClose }) {
  const token = localStorage.getItem("jwtToken");
  const [myOptions, setMyOptions] = useState([]);
  const [selectedValue, setSelectedValue] = useState(null);
  const [isSelectValid, setIsSelectValid] = useState(false);
  const [searchOption, setSearchOption] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const [showAddFriendForm, setShowAddFriendForm] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    if (searchOption) {
      getDataFromAPI();
    }
  }, [searchOption]);

  const getDataFromAPI = async () => {
    try {
      let response;
      switch (searchOption) {
        case "users":
          response = await axios.get('http://localhost:5183/api/User/GetUsers', {
            headers: {
              'Authorization': `Bearer ${token}`,
              'Accept': 'application/json'
            }
          });
          const dataUsers = response.data;
          const optionsUsers = dataUsers.map(user => ({ id: user.id, name: user.email }));
          setMyOptions(optionsUsers);
          break;
        case "projects":
          response = await axios.get('http://localhost:5170/api/Project/GetProjects', {
            headers: {
              'Authorization': `Bearer ${token}`,
              'Accept': 'application/json'
            }
          });
          const dataProject = response.data;
          const optionsProject = dataProject.map(project => ({ id: project.id, name: project.name }));
          setMyOptions(optionsProject);
          break;
        case "tasks":
          response = await axios.get('http://localhost:5170/api/ProjectTask/GetProjectTasksForUser', {
            headers: {
              'Authorization': `Bearer ${token}`,
              'Accept': 'application/json'
            }
          });
          const data = response.data;
          const optionsTasks = data.map(task => ({ id: task.id, name: task.name }));
          setMyOptions(optionsTasks);
          break;
        default:
          setMyOptions([]);
      }

    } catch (error) {
      console.error("Error fetching data: ", error);
    }
  };
  const handleSelectChange = (event) => {
    const value = event.target.value;
    setIsSelectValid(value !== "0");
    setErrorMessage('');
    setSearchOption(value);
  };

  const handleSearchClick = () => {
    if (selectedValue && isSelectValid) {
      switch (searchOption) {
        case "users":
          setShowAddFriendForm(true, myOptions);
          break;
        case "projects":
          navigate(`/projects/${selectedValue.id}`);
          break;
        case "tasks":
          break;
        default:
          break;
      }
    } else {
      setErrorMessage('Please fill out the form correctly!');
    }
  };


  return (
    <form className="search-window">
      <Modal isOpen={isOpen} onRequestClose={onRequestClose} className="react-modal-create" >
        <div className='react-modal-search'>
          <Autocomplete
            className='autocomplete'
            freeSolo
            autoComplete
            autoHighlight
            options={myOptions}
            getOptionLabel={(option) => option.name}
            onChange={(event, newValue) => setSelectedValue(newValue)}
            renderInput={(params) => (
              <TextField
                {...params}
                variant="outlined"
                label="Enter your query"
              />
            )}
          />
          <button onClick={handleSearchClick} className='search-but'>
            <i className="icon-magnifying" aria-hidden="true"></i>
          </button>
        </div>
        {errorMessage && <p className="error-message">{errorMessage}</p>}
        <select
          className={`status-select-search ${!isSelectValid ? 'invalid' : ''}`}
          required
          onChange={handleSelectChange}
        >
          <option key={0} value={0}>Choose where to search*</option>
          <option key={1} value={'users'}>Users</option>
          <option key={2} value={'projects'}>Projects</option>
          <option key={3} value={'tasks'}>Tasks</option>
        </select>
        {showAddFriendForm && <AddFriendForm isOpen={showAddFriendForm} onRequestClose={() => setShowAddFriendForm(false)} friend={selectedValue} />}
      </Modal>
    </form>
  );
}
