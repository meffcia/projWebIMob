using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Models;

namespace OnlineShop.Shared.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<CategoryDto>>> GetAllCategoriesAsync();
        Task<ServiceResponse<CategoryDto>> GetCategoryByIdAsync(int id);
        Task<ServiceResponse<Category>> AddCategoryAsync(CreateCategoryDto category);
        Task<ServiceResponse<Category>> UpdateCategoryAsync(int id, UpdateCategoryDto category);
        Task<ServiceResponse<bool>> DeleteCategoryAsync(int id);
    }
}
