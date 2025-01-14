using OnlineShop.Shared.DTOs;
using OnlineShop.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Api.Services
{
    public interface IOrderService
    {
        Task<ServiceResponse<List<OrderDto>>> GetAllOrdersAsync(int? userId, string? status, string? sortBy, bool descending, bool isAdmin);
        Task<ServiceResponse<OrderDto>> GetOrderByIdAsync(int id);
        Task<ServiceResponse<OrderDto>> AddOrderAsync(CreateOrderDto orderDto);
        Task<ServiceResponse<OrderDto>> UpdateOrderStatusAsync(int id, UpdateOrderDto updateDto);
        Task<ServiceResponse<bool>> DeleteOrderAsync(int id);
    }
}
