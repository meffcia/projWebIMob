import { useEffect, useState } from "react";
import * as signalR from '@microsoft/signalr';


function TicketList() {//}= ({ connection }) => {
  const [tickets, setTickets] = useState([]);

  useEffect(() => {

    const connection = new signalR.HubConnectionBuilder()
  .withUrl("/ticketHub") // URL backendu
  // .withAutomaticReconnect()
  // .configureLogging(signalR.LogLevel.Information)
  .build();

  connection.start()
  .then(() => console.log("SignalR Connected"))
  .catch(err => console.error("SignalR Connection Error: ", err));
  
    connection.on("ReceiveTicketUpdate", (ticket) => {
      // Po odebraniu nowego lub zaktualizowanego ticketu, dodaj go do stanu
      console.log("Received ticket update:", ticket);
      setTickets((prevTickets) => {
        const existingTicket = prevTickets.find((t) => t.id === ticket.id);
        if (existingTicket) {
          return prevTickets.map((t) => (t.id === ticket.id ? ticket : t));
        } else {
          return [...prevTickets, ticket];
        }
      });
    });

    // Pobieranie początkowych ticketów z API
    const fetchTickets = async () => {
        const response = await fetch('/api/ticket'); // Upewnij się, że port jest poprawny
        const data = await response.json();
        setTickets(data);
    };
        
    fetchTickets();

    return () => {
      connection.stop(); // Zamknięcie połączenia SignalR po zakończeniu komponentu
    };
  }, []);

  const updateTicketStatus = async (id, newStatus) => {
    try {
      // Znajdź aktualny ticket
      const ticketToUpdate = tickets.find(ticket => ticket.id === id);
      if (!ticketToUpdate) {
        console.error("Ticket not found in state");
        return;
      }
  
      // Utwórz obiekt z zaktualizowanym statusem
      const updatedTicket = { ...ticketToUpdate, status: newStatus };
  
      // Wyślij żądanie do API
      const response = await fetch(`/api/ticket/${id}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(updatedTicket),
      });
  
      if (response.ok) {
        console.log("Ticket updated successfully");
      } else {
        console.error("Failed to update ticket status:", response.statusText);
      }
    } catch (error) {
      console.error("Error updating ticket status:", error);
    }
  };

  return (
    <div>
      <h1>Tickets</h1>
      <ul>
        {tickets.map(ticket => (
          <li key={ticket.id}>
            <strong>{ticket.title}</strong> - {ticket.status}
            <p>{ticket.description}</p>
            {/* Przyciski do zmiany statusu */}
            <div>
              <button onClick={() => updateTicketStatus(ticket.id, "In Progress")}>
                Set In Progress
              </button>
              <button onClick={() => updateTicketStatus(ticket.id, "Resolved")}>
                Resolve
              </button>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default TicketList;

