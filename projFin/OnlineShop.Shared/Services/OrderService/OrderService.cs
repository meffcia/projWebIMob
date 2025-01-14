using OnlineShop.Shared;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.Services.OrderService;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineShop.Shared.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<List<OrderDto>>> GetAllOrdersAsync(int? userId, string? status, string? sortBy, bool descending)
        {
            var queryParams = new List<string>();

            if (userId.HasValue && userId > 0)
                queryParams.Add($"userId={userId}");

            if (!string.IsNullOrEmpty(status))
                queryParams.Add($"status={status}");

            if (!string.IsNullOrEmpty(sortBy))
                queryParams.Add($"sortBy={sortBy}");

            if (descending)
                queryParams.Add("descending=true");

            var queryString = queryParams.Any() ? "?" + string.Join("&", queryParams) : string.Empty;
            var url = $"api/orders{queryString}";

            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<OrderDto>>>(url);
            return response ?? new ServiceResponse<List<OrderDto>>
            {
                Success = false,
                Message = "Failed to fetch orders."
            };
        }

        public async Task<ServiceResponse<OrderDto>> GetOrderByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/orders/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<OrderDto>
                {
                    Success = false,
                    Message = "Order not found."
                };
            }

            var data = await response.Content.ReadFromJsonAsync<OrderDto>();
            return new ServiceResponse<OrderDto> { Data = data, Success = true };
        }

        public async Task<ServiceResponse<OrderDto>> AddOrderAsync(CreateOrderDto orderDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/orders", orderDto);

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<OrderDto>
                {
                    Success = false,
                    Message = "Failed to add order."
                };
            }

            var data = await response.Content.ReadFromJsonAsync<OrderDto>();
            return new ServiceResponse<OrderDto> { Data = data, Success = true };
        }

        public async Task<ServiceResponse<OrderDto>> UpdateOrderStatusAsync(int id, UpdateOrderDto updateDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/orders/{id}", updateDto);

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<OrderDto>
                {
                    Success = false,
                    Message = "Failed to update order."
                };
            }

            var data = await response.Content.ReadFromJsonAsync<OrderDto>();
            return new ServiceResponse<OrderDto> { Data = data, Success = true };
        }

        public async Task<ServiceResponse<bool>> DeleteOrderAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/orders/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Failed to delete order."
                };
            }

            return new ServiceResponse<bool> { Data = true, Success = true, Message = "Order deleted successfully." };
        }
    }
}
