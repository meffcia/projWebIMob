using System.ComponentModel.DataAnnotations;

namespace proj3.Models
{
    public class SaleModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł książki jest wymagany.")]
        public string BookTitle { get; set; }

        [Required(ErrorMessage = "Miesiąc jest wymagany.")]
        [Range(1, 12, ErrorMessage = "Miesiąc musi być w zakresie 1-12.")]
        public int Month { get; set; }  // Zmiana typu na int

        [Required(ErrorMessage = "Rok jest wymagany.")]
        [Range(2020, 2024, ErrorMessage = "Rok musi być w zakresie 2020-2024.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Ilość jest wymagana.")]
        [Range(1, 10000, ErrorMessage = "Ilość musi być większa niż 0.")]
        public int Quantity { get; set; }

    }
}
