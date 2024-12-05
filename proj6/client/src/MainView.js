import React from 'react';
import TicketForm from './components/TicketForm';
import TicketList from './components/TicketList';
import Notification from './components/Notification';
import * as signalR from '@microsoft/signalr';

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/ticketHub")
    .build();

const MainView = () => {
    return (
        <div>
            <h1>Real-Time Ticket Management</h1>
            <Notification connection={connection} />
            <TicketForm onTicketCreated={(ticket) => console.log('Ticket created:', ticket)} />
            <TicketList /*connection={connection} */isAdmin={false} />
        </div>
    );
};

export default MainView;
