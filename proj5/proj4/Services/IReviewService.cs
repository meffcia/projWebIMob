using proj5.Domain.Models;

namespace proj4.Services
{
    public interface IReviewService
    {
        Task<ServiceReponse<Review>> GetReviewByIdAsync(int id);
        Task<ServiceReponse<List<Review>>> GetAllReviewAsync();
        Task<ServiceReponse<Review>> AddReviewAsync(Review review);
        Task<ServiceReponse<Review>> UpdateReviewAsync(Review review);
        Task<ServiceReponse<Review>> DeleteReviewAsync(int id);
    }
}