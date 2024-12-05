import React, { useState } from 'react';

const TicketForm = ({ onTicketCreated }) => {
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();

        const newTicket = {
            title,
            description,
            status: 'New',
            priority: 'Medium',
        };

        const response = await fetch('http://localhost:5224/api/ticket', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newTicket),
        });

        if (response.ok) {
            const ticket = await response.json();
            onTicketCreated(ticket); // Wywołanie callbacku po stworzeniu zgłoszenia
            setTitle('');
            setDescription('');
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>Create a Ticket</h2>
            <input
                type="text"
                placeholder="Title"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                required
            />
            <textarea
                placeholder="Description"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                required
            />
            <button type="submit">Create Ticket</button>
        </form>
    );
};

export default TicketForm;