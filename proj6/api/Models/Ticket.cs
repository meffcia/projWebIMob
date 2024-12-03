using System;
using System.Collections.Generic;

namespace proj6.Models
{
    public class Ticket
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "New"; // New, InProgress, Resolved
        public string Priority { get; set; } = "Medium"; // Low, Medium, High
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }

    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
