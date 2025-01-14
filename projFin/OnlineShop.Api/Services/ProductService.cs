using Microsoft.EntityFrameworkCore;
using OnlineShop.Api.Data;
using OnlineShop.Shared;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.Services.ProductService;

namespace OnlineShop.Api.Services
{
    public class ProductService: IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<ProductsListDto>> GetAllProductsAsync(string? search, int? categoryId, string? sortBy, bool descending, int? pageNumber, int? pageSize)
        {
            var response = new ServiceResponse<ProductsListDto>();
            var query = _context.Products.AsQueryable();
            query = query.Where(p => p.Stock > 0);

            // Filtrowanie
            if (!string.IsNullOrEmpty(search))
            {
                var lowerSearch = search.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(lowerSearch) || p.Description.ToLower().Contains(lowerSearch));
            }
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            // Sortowanie
            query = sortBy?.ToLower() switch
            {
                "name" => descending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                "price" => descending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                _ => query.OrderBy(p => p.Id)
            };
            var totalItems = await query.CountAsync();

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value) // Pomijamy odpowiednią liczbę elementów
                    .Take(pageSize.Value);                        // Pobieramy tylko liczbę elementów na stronie
            }

            var products = await query
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    CategoryId = p.CategoryId
                })
                .ToListAsync();
            ProductsListDto productsList = new ProductsListDto {Products = products, TotalItems = totalItems};

            response.Data = productsList;

            response.Success = true;

            return response;
        }

        public async Task<ServiceResponse<ProductDto>> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)  // Pobieramy kategorię razem z produktem
                .Where(p => p.Id == id)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock,
                    CategoryId = p.CategoryId,
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return new ServiceResponse<ProductDto>
                {
                    Success = false,
                    Message = "Product not found."
                };
            }

            return new ServiceResponse<ProductDto> { Data = product, Success = true };
        }


        public async Task<ServiceResponse<Product>> AddProductAsync(CreateProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
                CategoryId = productDto.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ServiceResponse<Product>
            {
                Data = product,
                Success = true,
                Message = "Product added successfully."
            };
        }

        public async Task<ServiceResponse<Product>> UpdateProductAsync(int id, UpdateProductDto updateDto)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return new ServiceResponse<Product>
                {
                    Success = false,
                    Message = "Product not found."
                };
            }

            if (!string.IsNullOrEmpty(updateDto.Name)) product.Name = updateDto.Name;
            if (!string.IsNullOrEmpty(updateDto.Description)) product.Description = updateDto.Description;
            if (updateDto.Price.HasValue) product.Price = updateDto.Price.Value;
            if (updateDto.Stock.HasValue) product.Stock = updateDto.Stock.Value;
            if (updateDto.CategoryId.HasValue) product.CategoryId = updateDto.CategoryId.Value;

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new ServiceResponse<Product>
            {
                Data = product,
                Success = true,
                Message = "Product updated successfully."
            };
        }

        public async Task<ServiceResponse<bool>> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Product not found."
                };
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Success = true, Message = "Product deleted successfully." };
        }
    }
}
