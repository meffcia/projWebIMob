import React from 'react';
import TicketForm from './components/TicketForm';
import TicketList from './components/TicketList';
import Notification from './components/Notification';
// import connection from './signalR'
import * as signalR from '@microsoft/signalr';


const connection = new signalR.HubConnectionBuilder()
  .withUrl("http://localhost:5224/ticketHub", {
    transport: signalR.HttpTransportType.WebSockets, // Upewnij się, że WebSocket jest obsługiwany
    withCredentials: true,
  }) // URL backendu
  .withAutomaticReconnect()
  .configureLogging(signalR.LogLevel.Information)
  .build();

connection.start()
  .then(() => console.log("SignalR Connected"))
  .catch(err => console.error("SignalR Connection Error: ", err));


const App = () => {
  return (
    <div>
      <h1>Real-Time Ticket Management</h1>
      <Notification connection={connection} />
      <TicketForm onTicketCreated={(ticket) => console.log('Ticket created:', ticket)} />
      <TicketList connection={connection} />
    </div>
  );
};

export default App;
