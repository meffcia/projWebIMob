using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using proj6.Models;

namespace proj6.Hubs
{
    public class TicketHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Client connected: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }
    
        public async Task NotifyUpdate(Ticket ticket)
        {
            Console.WriteLine("Ticket sent to SignalR: " + JsonConvert.SerializeObject(ticket));

            await Clients.All.SendAsync("ReceiveTicketUpdate", ticket);
        }
    }
}
