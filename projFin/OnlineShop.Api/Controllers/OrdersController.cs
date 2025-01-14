using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared;
using OnlineShop.Shared.DTOs;
using OnlineShop.Api.Services;

namespace OnlineShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<OrderDto>>>> GetAllOrders(
            [FromQuery] int? userId = null,
            [FromQuery] string? status = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool descending = false)
        {
            bool isAdmin = User.IsInRole("Admin");
            var response = await _orderService.GetAllOrdersAsync(userId, status, sortBy, descending, isAdmin);
            return Ok(response);
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<OrderDto>>> GetOrderById(int id)
        {
            var response = await _orderService.GetOrderByIdAsync(id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<OrderDto>>> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            if (createOrderDto == null)
            {
                return BadRequest(new ServiceResponse<OrderDto>
                {
                    Success = false,
                    Message = "Invalid order data."
                });
            }

            var response = await _orderService.AddOrderAsync(createOrderDto);

            return CreatedAtAction(nameof(GetOrderById), new { id = response.Data.Id }, response);
        }

        // PUT: api/orders/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<OrderDto>>> UpdateOrderStatus(int id, [FromBody] UpdateOrderDto updateDto)
        {
            var response = await _orderService.UpdateOrderStatusAsync(id, updateDto);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var response = await _orderService.DeleteOrderAsync(id);
            if (!response.Success)
                return NotFound(response.Message);

            return NoContent();
        }
    }
}
