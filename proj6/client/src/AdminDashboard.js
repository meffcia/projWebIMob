import React from 'react';
import TicketList from './components/TicketList';
import * as signalR from '@microsoft/signalr';

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/ticketHub")
    .build();

const AdminDashboard = () => {
    return (
        <div>
            <h1>Panel Administratora</h1>
            <TicketList /*connection={connection}*/ isAdmin={true} />
        </div>
    );
};

export default AdminDashboard;
