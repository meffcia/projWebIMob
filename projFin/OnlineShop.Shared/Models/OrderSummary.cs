using System.Collections.Generic;

namespace OnlineShop.Shared.Models
{
    public class OrderSummary
    {
        public List<CartItem> CartItems { get; set; } // Elementy koszyka
        public decimal TotalAmount => CartItems?.Sum(item => item.Quantity * item.Product.Price) ?? 0; // Obliczanie sumy
        public List<string> PaymentMethods { get; set; } // Dostêpne metody p³atnoœci
    }
}
