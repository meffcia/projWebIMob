import React from 'react';
import TicketForm from './components/TicketForm';
import TicketList from './components/TicketList';

const MainView = () => {
    return (
        <div>
            <h1>Real-Time Ticket Management</h1>
            <TicketForm onTicketCreated={(ticket) => console.log('Ticket created:', ticket)} />
            <TicketList isAdmin={false} />
        </div>
    );
};

export default MainView;
