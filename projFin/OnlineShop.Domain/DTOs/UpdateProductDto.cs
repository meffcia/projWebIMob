using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Domain.DTOs
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, 999999.99, ErrorMessage = "Cena musi być liczbą od 0 do 999999.99.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Cena może mieć maksymalnie 2 cyfry po przecinku.")]
        public decimal? Price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Stan magazynowy musi być liczbą nieujemną.")]
        public int? Stock { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Identyfikator kategorii musi być większy od 0.")]
        public int? CategoryId { get; set; }
    }
}