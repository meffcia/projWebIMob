using Newtonsoft.Json;
using proj5.Domain.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace proj4.Services
{
    public class ReviewService : IReviewService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5062/api/Review";

        public ReviewService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Pobierz pisarza po ID
        public async Task<ServiceReponse<Review>> GetReviewByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<Review>($"Reviews/{id}");

            if (response == null)
            {
                return new ServiceReponse<Review>
                {
                    Success = false,
                    Message = "Review not found"
                };
            }

            return new ServiceReponse<Review>
            {
                Data = response,
                Success = true,
                Message = "Writer retrieved successfully"
            };
        }

        public async Task<ServiceReponse<List<Review>>> GetAllReviewAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(_apiBaseUrl);
                var reviews = JsonConvert.DeserializeObject<List<Review>>(response);
                return new ServiceReponse<List<Review>>
                {
                    Data = reviews,
                    Success = true,
                    Message = "Reviews retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<List<Review>>
                {
                    Message = $"Error loading reviews: {ex.Message}",
                    Success = false
                };
            }
        }

        public async Task<ServiceReponse<Review>> AddReviewAsync(Review review)
        {
            var response = await _httpClient.PostAsJsonAsync("Reviews", review);

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceReponse<Review>
                {
                    Success = false,
                    Message = "Failed to add review"
                };
            }

            var createdReview = await response.Content.ReadFromJsonAsync<Review>();

            return new ServiceReponse<Review>
            {
                Data = createdReview,
                Success = true,
                Message = "Review added successfully"
            };
        }

        public async Task<ServiceReponse<Review>> UpdateReviewAsync(Review review)
        {
            var response = await _httpClient.PutAsJsonAsync($"Reviews/{review.Id}", review);

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceReponse<Review>
                {
                    Success = false,
                    Message = "Failed to update review"
                };
            }

            var updatedReview = await response.Content.ReadFromJsonAsync<Review>();

            return new ServiceReponse<Review>
            {
                Data = updatedReview,
                Success = true,
                Message = "Review updated successfully"
            };
        }

        public async Task<ServiceReponse<Review>> DeleteReviewAsync(int id)
        {
                        var response = await _httpClient.DeleteAsync($"Reviews/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceReponse<Review>
                {
                    Success = false,
                    Message = "Failed to delete review"
                };
            }

            return new ServiceReponse<Review>
            {
                Success = true,
                Message = "Review deleted successfully"
            };
        }
    }
}
