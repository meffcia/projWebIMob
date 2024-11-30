using Newtonsoft.Json;
using proj5.Domain.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace proj4.Services
{
    public class WriterService : IWriterService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5062/api/writers";

        public WriterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Pobierz pisarza po ID
        public async Task<ServiceReponse<Writer>> GetWriterByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<Writer>($"Writers/{id}");

            if (response == null)
            {
                return new ServiceReponse<Writer>
                {
                    Success = false,
                    Message = "Writer not found"
                };
            }

            return new ServiceReponse<Writer>
            {
                Data = response,
                Success = true,
                Message = "Writer retrieved successfully"
            };
        }

        // Pobierz wszystkich pisarzy
        public async Task<ServiceReponse<List<Writer>>> GetAllWriterAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(_apiBaseUrl);
                var writers = JsonConvert.DeserializeObject<List<Writer>>(response);

                return new ServiceReponse<List<Writer>>
                {
                    Data = writers,
                    Success = true,
                    Message = "Writers retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<List<Writer>>
                {
                    Message = $"Error loading books: {ex.Message}",
                    Success = false
                };
            }
        }

        // Dodaj nowego pisarza
        public async Task<ServiceReponse<Writer>> AddWriterAsync(Writer writer)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(writer), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_apiBaseUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var createdWriter = JsonConvert.DeserializeObject<Writer>(await response.Content.ReadAsStringAsync());
                    return new ServiceReponse<Writer>
                    {
                        Data = createdWriter,
                        Success = true,
                        Message = "Writer added successfully"
                    };
                }

                return new ServiceReponse<Writer>
                {
                    Message = "Failed to add writer.",
                    Success = false
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<Writer>
                {
                    Message = $"Error adding writer: {ex.Message}",
                    Success = false
                };
            }
        }

        // Zaktualizuj pisarza
        public async Task<ServiceReponse<Writer>> UpdateWriterAsync(Writer writer)
        {
            var response = await _httpClient.PutAsJsonAsync($"Writers/{writer.Id}", writer);

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceReponse<Writer>
                {
                    Success = false,
                    Message = "Failed to update writer"
                };
            }

            var updatedWriter = await response.Content.ReadFromJsonAsync<Writer>();

            return new ServiceReponse<Writer>
            {
                Data = updatedWriter,
                Success = true,
                Message = "Writer updated successfully"
            };
        }

        // Usuń pisarza
        public async Task<ServiceReponse<Writer>> DeleteWriterAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Writers/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return new ServiceReponse<Writer>
                    {
                        Success = true,
                        Message = "Writer deleted successfully"
                    };
                }

                return new ServiceReponse<Writer>
                {
                    Success = false,
                    Message = "Failed to delete writer"
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<Writer>
                {
                    Message = $"Error deleting book: {ex.Message}",
                    Success = false
                };
            }
        }
    }
}
