using proj4.Models;

namespace proj4.Services
{
    public interface IProductService
    {
        Task<ServiceReponse<IProduct>> GetProductByIdAsync(int productId);
        Task<ServiceReponse<List<IProduct>>> GetAllProductAsync();
        Task<ServiceReponse<IProduct>> AddProductAsync(IProduct product);
        Task<ServiceReponse<IProduct>> UpdateProductAsync(IProduct product);
        Task<ServiceReponse<IProduct>> DeleteProductAsync(int id);
    }
}