using System.Net.Http.Json;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.Services;

namespace OnlineShop.Shared.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<ProductsListDto>> GetAllProductsAsync(string? search, int? categoryId, string? sortBy, bool descending, int? pageNumber, int? pageSize)
        {
            // Tworzenie zapytania z parametrami
            var queryParameters = new List<string>();

            if (!string.IsNullOrEmpty(search)) queryParameters.Add($"search={Uri.EscapeDataString(search)}");
            if (categoryId.HasValue) queryParameters.Add($"categoryId={categoryId.Value}");
            if (!string.IsNullOrEmpty(sortBy)) queryParameters.Add($"sortBy={Uri.EscapeDataString(sortBy)}");

            queryParameters.Add($"descending={descending.ToString().ToLower()}");

            if (pageNumber.HasValue) queryParameters.Add($"pageNumber={pageNumber}");
            if (pageSize.HasValue) queryParameters.Add($"pageSize={pageSize}");

            var queryString = string.Join("&", queryParameters);
            var url = $"api/products?{queryString}";

            // Wykonanie żądania HTTP
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<ProductsListDto>>(url);

            return response ?? new ServiceResponse<ProductsListDto>
            { 
                Success = false, 
                Message = "Failed to fetch products." 
            };
        }

        public async Task<ServiceResponse<ProductDto>> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<ProductDto>>($"api/products/{id}");
            return response ?? new ServiceResponse<ProductDto> { Success = false, Message = "Failed to fetch product details." };
        }

        public async Task<ServiceResponse<Product>> AddProductAsync(CreateProductDto product)
        {
            var response = await _httpClient.PostAsJsonAsync("api/products", product);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<Product>>() 
                   ?? new ServiceResponse<Product> { Success = false, Message = "Failed to add product." };
        }

        public async Task<ServiceResponse<Product>> UpdateProductAsync(int id, UpdateProductDto product)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/products/{id}", product);
            var responseContent = response.Content.ReadAsStream();
            Console.WriteLine("Response Data:");
            Console.WriteLine(responseContent);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<Product>>() 
                   ?? new ServiceResponse<Product> { Success = false, Message = "Failed to update product." };
        }

        public async Task<ServiceResponse<bool>> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/products/{id}");
            return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>() 
                   ?? new ServiceResponse<bool> { Success = false, Message = "Failed to delete product." };
        }
    }
}