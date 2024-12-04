public class Notification
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Message { get; set; }
    public string Type { get; set; } // NewTicket, CommentAdded, StatusUpdated, etc.
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid TicketId { get; set; } // Powiązanie z konkretnym zgłoszeniem
}
