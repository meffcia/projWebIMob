using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.Services.ProductService;

namespace OnlineShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<ProductsListDto>>> GetAllProducts(
            [FromQuery] string? search,
            [FromQuery] int? categoryId,
            [FromQuery] string? sortBy, 
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize,
            [FromQuery] bool descending = false)
            
        {
            var response = await _productService.GetAllProductsAsync(search, categoryId, sortBy, descending, pageNumber, pageSize);
            return Ok(response);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<ProductDto>>> GetProductById(int id)
        {
            var response = await _productService.GetProductByIdAsync(id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        // POST: api/products
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Product>>> AddProduct(CreateProductDto productDto)
        {
            var response = await _productService.AddProductAsync(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = response.Data.Id }, response);
        }

        // PUT: api/products/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<Product>>> UpdateProduct(int id, UpdateProductDto updateDto)
        {
            var response = await _productService.UpdateProductAsync(id, updateDto);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        // DELETE: api/products/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteProduct(int id)
        {
            var response = await _productService.DeleteProductAsync(id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }
    }
}