using Microsoft.EntityFrameworkCore;
using OnlineShop.Api.Data;
using OnlineShop.Shared;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.Services.CategoryService;

namespace OnlineShop.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            var response = new ServiceResponse<List<CategoryDto>>();

            try
            {
                // Pobierz wszystkie kategorie i zamień na CategoryDto
                response.Data = await _context.Categories
                    .Select(c => new CategoryDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description
                    })
                    .ToListAsync();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error fetching categories: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<CategoryDto>> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return new ServiceResponse<CategoryDto>
                {
                    Success = false,
                    Message = "Category not found."
                };
            }

            return new ServiceResponse<CategoryDto> { Data = category, Success = true };
        }

        public async Task<ServiceResponse<Category>> AddCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var category = new Category
            {
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new ServiceResponse<Category>
            {
                Data = category,
                Success = true,
                Message = "Category added successfully."
            };
        }

        public async Task<ServiceResponse<Category>> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return new ServiceResponse<Category>
                {
                    Success = false,
                    Message = "Category not found."
                };
            }

            // Aktualizuj dane kategorii, jeśli wartości są przekazane
            if (!string.IsNullOrEmpty(updateCategoryDto.Name)) category.Name = updateCategoryDto.Name;
            if (!string.IsNullOrEmpty(updateCategoryDto.Description)) category.Description = updateCategoryDto.Description;

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new ServiceResponse<Category>
            {
                Data = category,
                Success = true,
                Message = "Category updated successfully."
            };
        }

        public async Task<ServiceResponse<bool>> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Category not found."
                };
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Data = true,
                Success = true,
                Message = "Category deleted successfully."
            };
        }
    }
}