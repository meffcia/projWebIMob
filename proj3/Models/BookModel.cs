// Models/Book.cs
using System.ComponentModel.DataAnnotations;

namespace proj3.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Autor jest wymagany")]
        public string Author { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal Price { get; set; }
    }
}
