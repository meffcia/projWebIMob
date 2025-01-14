using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.Services.CategoryService;

namespace OnlineShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CategoryDto>>>> GetCategories()
        {
            var response = await _categoryService.GetAllCategoriesAsync();

            if (response.Success)
            {
                return Ok(response);  // Zwracamy dane jeśli sukces
            }

            return BadRequest(response);  // W przypadku błędu, zwracamy BadRequest
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<CategoryDto>>> GetCategory(int id)
        {
            var response = await _categoryService.GetCategoryByIdAsync(id);

            if (response.Success)
            {
                return Ok(response);  // Zwracamy dane jeśli sukces
            }

            return NotFound(response);  // Kategoria nie została znaleziona
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Category>>> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var response = await _categoryService.AddCategoryAsync(createCategoryDto);

            if (response.Success)
            {
                return CreatedAtAction(nameof(GetCategory), new { id = response.Data.Id }, response);  // Zwracamy CreatedAt z ID nowej kategorii
            }

            return BadRequest(response);  // Jeśli wystąpił błąd, zwracamy BadRequest
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<Category>>> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            var response = await _categoryService.UpdateCategoryAsync(id, updateCategoryDto);

            if (response.Success)
            {
                return NoContent();  // Zwracamy NoContent po pomyślnej aktualizacji
            }

            return NotFound(response);  // Kategoria nie została znaleziona
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = await _categoryService.DeleteCategoryAsync(id);

            if (response.Success)
            {
                return NoContent();  // Zwracamy NoContent po pomyślnym usunięciu
            }

            return NotFound(response);  // Kategoria nie została znaleziona
        }
    }
}