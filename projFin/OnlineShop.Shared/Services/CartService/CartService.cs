using System.Net.Http;
using System.Net.Http.Json;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared;
using Azure;
using OnlineShop.Shared.Services.CartService;

namespace OnlineShop.Shared.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<List<CartItem>>> GetCartItemsAsync(int userId)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<CartItem>>>($"api/cart/{userId}");
            return response ?? new ServiceResponse<List<CartItem>> { Success = false, Message = "Failed to get cart."};
        }

        public async Task<ServiceResponse<CartItem>> AddToCartAsync(int userId, AddCartItemDto cartItemDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/cart/{userId}", cartItemDto);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<CartItem>>()
                    ?? new ServiceResponse<CartItem> { Success = false, Message = "Failed to get cart."};
        }

        public async Task<ServiceResponse<bool>> RemoveFromCartAsync(int userId, int productId)
        {
            var response = await _httpClient.DeleteAsync($"api/cart/{userId}/{productId}");
            return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>()
                    ?? new ServiceResponse<bool> { Success = false, Message = "Failed to remove the item." };
        }

        public async Task<ServiceResponse<bool>> ClearCartAsync(int userId)
        {
            var response = await _httpClient.DeleteAsync($"api/cart/{userId}/clear");
            return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>()
                    ?? new ServiceResponse<bool> { Success = false, Message = "Failed to clear the cart." };
        }
    }
}
