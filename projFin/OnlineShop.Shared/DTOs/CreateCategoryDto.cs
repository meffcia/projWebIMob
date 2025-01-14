using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Shared.DTOs
{
    public class CreateCategoryDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; }
        
        [StringLength(200, ErrorMessage = "Description must be up to 200 characters.")]
        public string Description { get; set; } = "";
    }
}