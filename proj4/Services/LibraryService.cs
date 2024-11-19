using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using proj4.Confguration;
using proj4.Models;
using System.Net.Http.Json;

namespace proj4.Services
{
    public class LibraryService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public LibraryService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
        }

        public async Task<ServiceReponse<IProduct>> AddProductAsync(IProduct product)
        {
            var response = await _httpClient.PostAsJsonAsync(_appSettings.ProductEndpoint.CreateProduct, product);
            var result = await response.Content.ReadFromJsonAsync<ServiceReponse<IProduct>>();
            return result;
        }

        // public async Task DeleteProductAsync(int id)
        // {
        //     var response = await _httpClient.DeleteAsync($"{id}");
        //     var result = await response.Content.ReadFromJsonAsync<ServiceReponse<bool>>();
        //     return result;
        // }

        public async Task<ServiceReponse<List<IProduct>>> GetAllProductAsync()
        {
            var response = await _httpClient.GetAsync(_appSettings.ProductEndpoint.GetProducts);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceReponse<List<IProduct>>>(json);
            return result;
        }

        public async Task<ServiceReponse<IProduct>> UpdateProductAsync(IProduct product)
        {
            var response = await _httpClient.PutAsJsonAsync(_appSettings.ProductEndpoint.UpdateProduct, product);
            var result = await response.Content.ReadFromJsonAsync<ServiceReponse<IProduct>>();
            return result;
        }
    }
}