using System.Net.Http.Json;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.Services;

namespace OnlineShop.Shared.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<CategoryDto>>>("api/categories");
                return response ?? new ServiceResponse<List<CategoryDto>> { Success = false, Message = "Failed to fetch categories." };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<CategoryDto>> { Success = false, Message = $"Error fetching categories: {ex.Message}" };
            }
        }

        public async Task<ServiceResponse<CategoryDto>> GetCategoryByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ServiceResponse<CategoryDto>>($"api/categories/{id}");
                return response ?? new ServiceResponse<CategoryDto> { Success = false, Message = $"Category with ID {id} not found." };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<CategoryDto> { Success = false, Message = $"Error fetching category: {ex.Message}" };
            }
        }

        public async Task<ServiceResponse<Category>> AddCategoryAsync(CreateCategoryDto category)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/categories", category);
                if (response.IsSuccessStatusCode)
                {
                    var addedCategory = await response.Content.ReadFromJsonAsync<Category>();
                    return new ServiceResponse<Category> { Data = addedCategory, Success = true, Message = "Category added successfully." };
                }
                return new ServiceResponse<Category> { Success = false, Message = "Failed to add category." };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Category> { Success = false, Message = $"Error adding category: {ex.Message}" };
            }
        }

        public async Task<ServiceResponse<Category>> UpdateCategoryAsync(int id, UpdateCategoryDto category)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/categories/{id}", category);
                if (response.IsSuccessStatusCode)
                {
                    var updatedCategory = await response.Content.ReadFromJsonAsync<Category>();
                    return new ServiceResponse<Category> { Data = updatedCategory, Success = true, Message = "Category updated successfully." };
                }
                return new ServiceResponse<Category> { Success = false, Message = "Failed to update category." };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Category> { Success = false, Message = $"Error updating category: {ex.Message}" };
            }
        }

        public async Task<ServiceResponse<bool>> DeleteCategoryAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/categories/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return new ServiceResponse<bool> { Data = true, Success = true, Message = "Category deleted successfully." };
                }
                return new ServiceResponse<bool> { Data = false, Success = false, Message = "Failed to delete category." };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool> { Data = false, Success = false, Message = $"Error deleting category: {ex.Message}" };
            }
        }
    }
}