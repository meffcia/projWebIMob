import { useEffect, useState } from "react";
import * as signalR from '@microsoft/signalr';


const TicketList = ({ /*connection,*/ isAdmin }) => {
  const [tickets, setTickets] = useState([]);
  const [resolvedTickets, setResolvedTickets] = useState([]);

  useEffect(() => {

    const connection = new signalR.HubConnectionBuilder()
  .withUrl("/ticketHub") // URL backendu
  // // .withAutomaticReconnect()
  // // .configureLogging(signalR.LogLevel.Information)
  .build();

  connection.start()
    .then(() => console.log("SignalR Connected"))
    .catch(err => console.error("SignalR Connection Error: ", err));
  
    connection.on("ReceiveTicketUpdate", (ticket) => {
      // Po odebraniu nowego lub zaktualizowanego ticketu, dodaj go do stanu
      console.log("Received ticket update:", ticket);
      if (ticket.status === "Resolved") {
        // Przenieś ticket do resolvedTickets
        setTickets((prevTickets) => prevTickets.filter((t) => t.id !== ticket.id));
        setResolvedTickets((prevResolved) => [...prevResolved, ticket]);
      } else {
        // Zaktualizuj w liście aktywnych ticketów
        setTickets((prevTickets) => {
          const existingTicket = prevTickets.find((t) => t.id === ticket.id);
          if (existingTicket) {
            return prevTickets.map((t) => (t.id === ticket.id ? ticket : t));
          } else {
            return [...prevTickets, ticket];
          }
        });
      }
    });

    // Pobieranie początkowych ticketów z API
    const fetchTickets = async () => {
      const response = await fetch('/api/ticket'); // Upewnij się, że port jest poprawny
      const data = await response.json();
      setTickets(data.filter((ticket) => ticket.status !== "Resolved"));
      setResolvedTickets(data.filter((ticket) => ticket.status === "Resolved"));
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
      <h2>Active Tickets</h2>
      <ul>
        {tickets.map((ticket) => (
          <li key={ticket.id}>
            <strong>{ticket.title}</strong> - {ticket.status}
            <p>{ticket.description}</p>
            {isAdmin && (
              <div>
                <button onClick={() => updateTicketStatus(ticket.id, "In Progress")}>
                  Set In Progress
                </button>
                <button onClick={() => updateTicketStatus(ticket.id, "Resolved")}>
                  Resolve
                </button>
              </div>
            )}
          </li>
        ))}
      </ul>

      {resolvedTickets.length > 0 && (
        <>
          <h2>Resolved Tickets</h2>
          <ul>
            {resolvedTickets.map((ticket) => (
              <li key={ticket.id}>
                <strong>{ticket.title}</strong> - {ticket.status}
                <p>{ticket.description}</p>
              </li>
            ))}
          </ul>
        </>
      )}
    </div>
  );
};

export default TicketList;