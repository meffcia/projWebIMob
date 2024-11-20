
using System.Net.Http.Json;
using ClientApp.Models;

namespace ClientApp.Services
{
    public class ApiProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ApiProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<IProduct>> GetAllProductAsync()
        {
            var products = await _httpClient.GetFromJsonAsync<List<Product>>("api/products");
            return products.Cast<IProduct>().ToList();
        }

        public async Task<IProduct> AddProductAsync(IProduct product)
        {
            var response = await _httpClient.PostAsJsonAsync("api/products", product);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Product>();
        }

        public async Task<IProduct> UpdateProductAsync(IProduct product)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/products/{product.Id}", product);
            response.EnsureSuccessStatusCode();
            return product;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/products/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
