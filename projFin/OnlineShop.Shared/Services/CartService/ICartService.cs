using OnlineShop.Shared;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Models;

namespace OnlineShop.Shared.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartItem>>> GetCartItemsAsync(int userId);
        Task<ServiceResponse<CartItem>> AddToCartAsync(int userId, AddCartItemDto cartItemDto);
        Task<ServiceResponse<bool>> RemoveFromCartAsync(int userId, int productId);
        Task<ServiceResponse<bool>> ClearCartAsync(int userId);
    }
}