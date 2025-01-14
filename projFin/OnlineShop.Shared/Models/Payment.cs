namespace OnlineShop.Shared.Models
{
        public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } // CreditCard, PayPal, BankTransfer
        public string Status { get; set; } // Pending, Completed, Failed
        public decimal Amount { get; set; }
    }
}