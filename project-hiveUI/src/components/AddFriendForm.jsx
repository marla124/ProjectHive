import { useState, useEffect } from 'react';
import Modal from 'react-modal';
import axios from 'axios';
import "../styles/addFriendWindow.css";

export default function AddFriendForm({ isOpen, onRequestClose, friend }) {
    const token = localStorage.getItem("jwtToken");
    const [isButtonDisabled, setIsButtonDisabled] = useState(false);
    const handleAddFriend = async () => {
        try {
            setIsButtonDisabled(true);
            await axios.post(`http://localhost:5183/api/User/AddFriendlyUser/${friend.id}`, {}, {
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Accept': 'application/json',
                },
            });
            alert('Success');
        } catch (error) {
            console.error('Ошибка при выполнении запроса', error);
            alert('Please try again.');
            setIsButtonDisabled(false);
        }
    };

    return (
        <div className="add-friend-window">
            <Modal isOpen={isOpen} onRequestClose={onRequestClose} className="react-modal-add-friend">
                <div className='add-friend-container'>
                    <p>{friend.name}</p>
                    <button className="button-form-add" onClick={handleAddFriend} disabled={isButtonDisabled}>
                        <i className="icon-plus icon-plus-for-addfriend" aria-hidden="true"></i>
                        Follow</button>
                </div>
            </Modal>
        </div>
    );
}