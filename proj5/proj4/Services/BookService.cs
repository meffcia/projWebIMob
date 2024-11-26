using Newtonsoft.Json;
using proj4.Models;
using proj4.Services;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace proj4.Services
{
    public class BookService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5062/api/books"; 

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceReponse<IProduct>> GetProductByIdAsync(int productId)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{_apiBaseUrl}/{productId}");
                var product = JsonConvert.DeserializeObject<Book>(response);
                return new ServiceReponse<IProduct>
                {
                    Data = product,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<IProduct>
                {
                    Message = $"Error retrieving product: {ex.Message}",
                    Success = false
                };
            }
        }

        public async Task<ServiceReponse<List<IProduct>>> GetAllProductAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(_apiBaseUrl);
                var products = JsonConvert.DeserializeObject<List<Book>>(response);
                return new ServiceReponse<List<IProduct>>
                {
                    Data = products.Cast<IProduct>().ToList(),
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<List<IProduct>>
                {
                    Message = $"Error loading products: {ex.Message}",
                    Success = false
                };
            }
        }

        public async Task<ServiceReponse<IProduct>> AddProductAsync(IProduct product)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_apiBaseUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var newProduct = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
                    return new ServiceReponse<IProduct>
                    {
                        Data = newProduct,
                        Success = true
                    };
                }

                return new ServiceReponse<IProduct>
                {
                    Message = "Failed to add product.",
                    Success = false
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<IProduct>
                {
                    Message = $"Error adding product: {ex.Message}",
                    Success = false
                };
            }
        }

        public async Task<ServiceReponse<IProduct>> UpdateProductAsync(IProduct product)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{product.Id}", content);
                if (response.IsSuccessStatusCode)
                {
                    var updatedProduct = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
                    return new ServiceReponse<IProduct>
                    {
                        Data = updatedProduct,
                        Success = true
                    };
                }

                return new ServiceReponse<IProduct>
                {
                    Message = "Failed to update product.",
                    Success = false
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<IProduct>
                {
                    Message = $"Error updating product: {ex.Message}",
                    Success = false
                };
            }
        }

        public async Task<ServiceReponse<IProduct>> DeleteProductAsync(int productId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{productId}");
                if (response.IsSuccessStatusCode)
                {
                    return new ServiceReponse<IProduct>
                    {
                        Success = true
                    };
                }

                return new ServiceReponse<IProduct>
                {
                    Message = "Failed to delete product.",
                    Success = false
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<IProduct>
                {
                    Message = $"Error deleting product: {ex.Message}",
                    Success = false
                };
            }
        }
    }
}
