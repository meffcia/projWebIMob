using proj5.Domain.Models;

namespace proj4.Services
{
    public interface IProductService
    {
        Task<ServiceReponse<Book>> GetProductByIdAsync(int productId);
        Task<ServiceReponse<List<Book>>> GetAllProductAsync();
        Task<ServiceReponse<Book>> AddProductAsync(Book product);
        Task<ServiceReponse<Book>> UpdateProductAsync(Book product);
        Task<ServiceReponse<Book>> DeleteProductAsync(int id);
    }
}