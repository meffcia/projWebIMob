using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using proj6.Models;

namespace proj6.Hubs
{
    public class TicketHub : Hub
    {
        // Metoda wywoływana, gdy klient się połączy
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Client connected: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        // Metoda wysyłająca zaktualizowany bilet do wszystkich połączonych klientów
        public async Task NotifyUpdate(Ticket ticket)
        {
            Console.WriteLine("Ticket sent to SignalR: " + JsonConvert.SerializeObject(ticket));

            // Wysyłanie zaktualizowanego biletu do wszystkich klientów
            await Clients.All.SendAsync("ReceiveTicketUpdate", ticket);
        }
    }
}
