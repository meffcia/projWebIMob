using Microsoft.AspNetCore.SignalR;

namespace proj6.Hubs
{
    public class TicketHub : Hub
    {
        public async Task NotifyUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveUpdate", message);
        }
    }
}
