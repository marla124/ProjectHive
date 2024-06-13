import React, { useEffect, useState } from 'react';
import Navbar from './Navbar';
import "../styles/notification.css";
import axios from "axios";

export default function Notification() {
  const [books, setBooks] = useState(null);

  const fetchData = async () => {
    const response = await axios.get(""); 
    setBooks(response.data);
    console.log(response.data);
  }

  useEffect(() => {
    fetchData();
  }, []);

return (
    <div className="block">
      <Navbar />
      {books && books.map((book, index) => (
        <div key={index}>
          <h2>{book.title}</h2>
          <p>{book.description}</p>
        </div>
      ))}
    </div>
  );
}