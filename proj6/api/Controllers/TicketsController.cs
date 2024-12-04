using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using proj6.Data;
using proj6.Hubs;
using proj6.Models;

namespace proj6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<TicketHub> _hubContext;

        public TicketController(AppDbContext context, IHubContext<TicketHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET method to retrieve all tickets (this is what GetTickets should reference)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            var tickets = await _context.Tickets.ToListAsync();
            return Ok(tickets);
        }

        // POST method to create a new ticket
        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] Ticket ticket)
        {
            // Automatycznie przypisanie daty utworzenia i aktualizacji
            ticket.CreatedAt = DateTime.UtcNow;
            ticket.UpdatedAt = DateTime.UtcNow;

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            // Powiadomienie przez SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveTicketUpdate", ticket);

            // Use CreatedAtAction to return the newly created ticket with a status code 201
            return CreatedAtAction(nameof(GetTickets), new { id = ticket.Id }, ticket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(Guid id, [FromBody] Ticket updatedTicket)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            // Update the ticket properties
            ticket.Status = updatedTicket.Status;
            ticket.UpdatedAt = DateTime.UtcNow;

            _context.Entry(ticket).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Powiadomienie przez SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveTicketUpdate", $"Ticket updated: {ticket.Title}");

            return NoContent(); // Successful update with no content returned
        }

        // POST method to create a new comment
        [HttpPost("comment")]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment)
        {
            // Automatycznie przypisanie daty utworzenia i Id
            comment.CreatedAt = DateTime.UtcNow;
            comment.Id = Guid.NewGuid(); // Automatyczne przypisanie ID

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return Ok(comment);
        }
    }

}
