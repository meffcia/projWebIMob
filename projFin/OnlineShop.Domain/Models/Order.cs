namespace OnlineShop.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } // Pending, Shipped, Delivered, Cancelled
        public decimal TotalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}