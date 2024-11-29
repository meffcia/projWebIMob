using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using proj5.Domain.Models;

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

        // Pobierz książkę na podstawie ID
        public async Task<ServiceReponse<Book>> GetProductByIdAsync(int bookId)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{_apiBaseUrl}/{bookId}");
                var book = JsonConvert.DeserializeObject<Book>(response);
                return new ServiceReponse<Book>
                {
                    Data = book,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<Book>
                {
                    Message = $"Error retrieving book: {ex.Message}",
                    Success = false
                };
            }
        }

        // Pobierz wszystkie książki
        public async Task<ServiceReponse<List<Book>>> GetAllProductAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(_apiBaseUrl);
                var books = JsonConvert.DeserializeObject<List<Book>>(response);
                return new ServiceReponse<List<Book>>
                {
                    Data = books,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<List<Book>>
                {
                    Message = $"Error loading books: {ex.Message}",
                    Success = false
                };
            }
        }

        // Dodaj nową książkę
        public async Task<ServiceReponse<Book>> AddProductAsync(Book book)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_apiBaseUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var newBook = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
                    return new ServiceReponse<Book>
                    {
                        Data = newBook,
                        Success = true
                    };
                }

                return new ServiceReponse<Book>
                {
                    Message = "Failed to add book.",
                    Success = false
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<Book>
                {
                    Message = $"Error adding book: {ex.Message}",
                    Success = false
                };
            }
        }

        // Aktualizuj książkę
        public async Task<ServiceReponse<Book>> UpdateProductAsync(Book book)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{book.Id}", content);
                if (response.IsSuccessStatusCode)
                {
                    var updatedBook = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
                    return new ServiceReponse<Book>
                    {
                        Data = updatedBook,
                        Success = true
                    };
                }

                return new ServiceReponse<Book>
                {
                    Message = "Failed to update book.",
                    Success = false
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<Book>
                {
                    Message = $"Error updating book: {ex.Message}",
                    Success = false
                };
            }
        }

        // Usuń książkę
        public async Task<ServiceReponse<Book>> DeleteProductAsync(int bookId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{bookId}");
                if (response.IsSuccessStatusCode)
                {
                    return new ServiceReponse<Book>
                    {
                        Success = true
                    };
                }

                return new ServiceReponse<Book>
                {
                    Message = "Failed to delete book.",
                    Success = false
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<Book>
                {
                    Message = $"Error deleting book: {ex.Message}",
                    Success = false
                };
            }
        }
    }
}
