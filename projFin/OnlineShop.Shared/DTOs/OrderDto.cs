namespace OnlineShop.Shared.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
