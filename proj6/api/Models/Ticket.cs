using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace proj6.Models
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "New"; // New, InProgress, Resolved

        [Required]
        [MaxLength(10)]
        public string Priority { get; set; } = "Medium"; // Low, Medium, High

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Comment> Comments { get; set; }

        public Ticket()
        {
            Comments = new List<Comment>();  // Inicjalizacja w konstruktorze
        }
    }

    public class Comment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(50)]
        public string Author { get; set; }

        [Required]
        [MaxLength(300)]
        public string Text { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Klucz obcy do Ticket
        [ForeignKey("Ticket")]
        public Guid TicketId { get; set; }

        // Nawigacja do zgłoszenia - ignorowane w JSON-ie
        [JsonIgnore] // Ignoruje to pole w przesyłanym i zwracanym JSON-ie
        public Ticket? Ticket { get; set; }
    }
}
