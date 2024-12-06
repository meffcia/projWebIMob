import React from 'react';
import TicketList from './components/TicketList';

const AdminDashboard = () => {
    return (
        <div>
            <h1>Panel Administratora</h1>
            <TicketList isAdmin={true} />
        </div>
    );
};

export default AdminDashboard;
