using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Models;

namespace OnlineShop.Shared.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<ProductsListDto>> GetAllProductsAsync(string? search, int? categoryId, string? sortBy, bool descending, int? pageNumber, int? pageSize);
        Task<ServiceResponse<ProductDto>> GetProductByIdAsync(int id);
        Task<ServiceResponse<Product>> AddProductAsync(CreateProductDto product);
        Task<ServiceResponse<Product>> UpdateProductAsync(int id, UpdateProductDto product);
        Task<ServiceResponse<bool>> DeleteProductAsync(int id);
    }
}
