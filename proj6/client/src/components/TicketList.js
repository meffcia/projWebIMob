import { useEffect, useState } from "react";

function TicketList({ connection }) { // Teraz otrzymujemy connection przez props
    const [tickets, setTickets] = useState([]);

    useEffect(() => {
        // Nasłuchujemy na zdarzenie 'ReceiveTicketUpdate' z serwera SignalR
        connection.on("ReceiveTicketUpdate", (ticket) => {
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
            // Zamknięcie połączenia SignalR po zakończeniu komponentu
            connection.off("ReceiveTicketUpdate"); // Usunięcie nasłuchiwania przy unmount
        };
    }, [connection]); // Dodajemy connection jako zależność, by upewnić się, że jest używane odpowiednio

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
