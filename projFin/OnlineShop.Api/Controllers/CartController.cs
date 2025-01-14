using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared;
using OnlineShop.Shared.Services.CartService;

namespace OnlineShop.Api.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ServiceResponse<List<CartItem>>>> GetCartItems(int userId)
        {
            var response = await _cartService.GetCartItemsAsync(userId);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("{userId}")]
        public async Task<ActionResult<ServiceResponse<CartItem>>> AddToCart(int userId, [FromBody] AddCartItemDto cartItemDto)
        {
            var response = await _cartService.AddToCartAsync(userId, cartItemDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{userId}/{productId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveFromCart(int userId, int productId)
        {
            var response = await _cartService.RemoveFromCartAsync(userId, productId);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{userId}/clear")]
        public async Task<ActionResult<ServiceResponse<bool>>> ClearCart(int userId)
        {
            var response = await _cartService.ClearCartAsync(userId);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
