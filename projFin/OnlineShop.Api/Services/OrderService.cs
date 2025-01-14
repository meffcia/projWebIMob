using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using OnlineShop.Api.Data;
using OnlineShop.Shared;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Models;
using OnlineShop.Api.Services;

namespace OnlineShop.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<OrderDto>>> GetAllOrdersAsync(int? userId, string? status, string? sortBy, bool descending, bool isAdmin)
        {
            var query = _context.Orders.AsQueryable();
            // Sprawdzamy, czy użytkownik ma rolę admin
            if (isAdmin)
            {
                // Admin widzi wszystkie zamówienia
                query = _context.Orders.AsQueryable();
            }
            else
            {
                // Jeśli użytkownik nie jest adminem, widzi tylko swoje zamówienia
                if (userId.HasValue && userId > 0)
                {
                    query = query.Where(o => o.UserId == userId);
                }
            }
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            // Sortowanie
            query = sortBy?.ToLower() switch
            {
                "orderdate" => descending ? query.OrderByDescending(o => o.OrderDate) : query.OrderBy(o => o.OrderDate),
                "totalamount" => descending ? query.OrderByDescending(o => o.TotalAmount) : query.OrderBy(o => o.TotalAmount),
                _ => query.OrderBy(o => o.Id)
            };

            var data = await query
                .Include(o => o.OrderItems)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                    {
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice
                    }).ToList()
                }).ToListAsync();

            return  new ServiceResponse<List<OrderDto>> { Data = data, Success = true };
        }

        public async Task<ServiceResponse<OrderDto>> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)  // Pobieramy produkty razem z pozycjami zamówienia
                .Where(o => o.Id == id)
                .Select(o => new OrderDto
                {
                    Id = id,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                    {
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return new ServiceResponse<OrderDto>
                {
                    Success = false,
                    Message = "Order not found."
                };
            }

            return new ServiceResponse<OrderDto> { Data = order, Success = true };
        }

        public async Task<ServiceResponse<OrderDto>> AddOrderAsync(CreateOrderDto createOrderDto)
        {
             // Aktualizacja stanu magazynowego
            foreach (var orderItem in createOrderDto.OrderItems)
            {
                var product = await _context.Products.FindAsync(orderItem.ProductId);
                product.Stock -= orderItem.Quantity;
            }
            var order = new Order
            {
                UserId = createOrderDto.UserId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending", // Domyślny status
                OrderItems = createOrderDto.OrderItems.Select(oi => new OrderItem
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };

            order.TotalAmount = order.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderDto = new OrderDto
            {
                Id = order.Id,
                OrderItems = createOrderDto.OrderItems
            };

            return new ServiceResponse<OrderDto>
            {
                Data = orderDto,
                Success = true,
                Message = "Order created successfully."
            };
        }

        public async Task<ServiceResponse<OrderDto>> UpdateOrderStatusAsync(int id, UpdateOrderDto updateDto)
        {
            var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return new ServiceResponse<OrderDto>
                {
                    Success = false,
                    Message = "Order not found."
                };
            }

            if (!string.IsNullOrEmpty(updateDto.Status))
                order.Status = updateDto.Status;

            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new ServiceResponse<OrderDto>
            {
                Data = new OrderDto
                {
                    Id = id,
                    OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                    {
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice
                    }).ToList()
                },
                Success = true,
                Message = "Order updated successfully."
            };
        }

        public async Task<ServiceResponse<bool>> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Order not found."
                };
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Success = true, Message = "Order deleted successfully." };
        }
    }
}
