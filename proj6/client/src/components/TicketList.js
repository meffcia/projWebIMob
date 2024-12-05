import { useEffect, useState } from "react";


const TicketList = ({ connection }) => {
  const [tickets, setTickets] = useState([]);

  useEffect(() => {

    connection.start()
      .then(() => console.log("SignalR connected"))
      .catch(err => console.log("SignalR connection error: ", err));

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
        const response = await fetch('http://localhost:5224/api/ticket'); // Upewnij się, że port jest poprawny
        const data = await response.json();
        setTickets(data);
    };
        
    fetchTickets();

    return () => {
      connection.stop(); // Zamknięcie połączenia SignalR po zakończeniu komponentu
    };
  }, []);

  return (
    <div>
      <h1>Tickets</h1>
      <ul>
        {tickets.map(ticket => (
          <li key={ticket.id}>
            <strong>{ticket.title}</strong> - {ticket.status}
            <p>{ticket.description}</p>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default TicketList;

