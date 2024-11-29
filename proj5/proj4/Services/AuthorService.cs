using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using proj5.Domain.Models;

namespace proj4.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5062/api/authors"; // Zmieniamy endpoint na /authors

        public AuthorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Pobierz autora na podstawie ID
        public async Task<ServiceReponse<Author>> GetAuthorByIdAsync(int authorId)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{_apiBaseUrl}/{authorId}");
                var author = JsonConvert.DeserializeObject<Author>(response);
                return new ServiceReponse<Author>
                {
                    Data = author,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<Author>
                {
                    Message = $"Error retrieving author: {ex.Message}",
                    Success = false
                };
            }
        }

        // Pobierz wszystkich autorów
        public async Task<ServiceReponse<List<Author>>> GetAllAuthorAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(_apiBaseUrl);
                var authors = JsonConvert.DeserializeObject<List<Author>>(response);
                return new ServiceReponse<List<Author>>
                {
                    Data = authors,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<List<Author>>
                {
                    Message = $"Error loading authors: {ex.Message}",
                    Success = false
                };
            }
        }

        // Dodaj nowego autora
        public async Task<ServiceReponse<Author>> AddAuthorAsync(Author author)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(author), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_apiBaseUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var newAuthor = JsonConvert.DeserializeObject<Author>(await response.Content.ReadAsStringAsync());
                    return new ServiceReponse<Author>
                    {
                        Data = newAuthor,
                        Success = true
                    };
                }

                return new ServiceReponse<Author>
                {
                    Message = "Failed to add author.",
                    Success = false
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<Author>
                {
                    Message = $"Error adding author: {ex.Message}",
                    Success = false
                };
            }
        }

        // Aktualizuj autora
        public async Task<ServiceReponse<Author>> UpdateAuthorAsync(Author author)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(author), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{author.Id}", content);
                if (response.IsSuccessStatusCode)
                {
                    var updatedAuthor = JsonConvert.DeserializeObject<Author>(await response.Content.ReadAsStringAsync());
                    return new ServiceReponse<Author>
                    {
                        Data = updatedAuthor,
                        Success = true
                    };
                }

                return new ServiceReponse<Author>
                {
                    Message = "Failed to update author.",
                    Success = false
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<Author>
                {
                    Message = $"Error updating author: {ex.Message}",
                    Success = false
                };
            }
        }

        // Usuń autora
        public async Task<ServiceReponse<Author>> DeleteAuthorAsync(int authorId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{authorId}");
                if (response.IsSuccessStatusCode)
                {
                    return new ServiceReponse<Author>
                    {
                        Success = true
                    };
                }

                return new ServiceReponse<Author>
                {
                    Message = "Failed to delete author.",
                    Success = false
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<Author>
                {
                    Message = $"Error deleting author: {ex.Message}",
                    Success = false
                };
            }
        }
    }
}
