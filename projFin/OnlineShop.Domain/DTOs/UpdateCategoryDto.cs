using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Domain.DTOs
{
    public class UpdateCategoryDto
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; } = null;
        
        [StringLength(200, ErrorMessage = "Description must be up to 200 characters.")]
        public string Description { get; set; } = null;
    }
}