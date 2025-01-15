using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared.Models;

namespace OnlineShop.WebApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly List<CartItem> _cartItems;

        public PaymentController()
        {
            // Przyk³adowe dane koszyka
            _cartItems = new List<CartItem>
            {
                new CartItem
                {
                    Id = 1,
                    UserId = 1,
                    ProductId = 101,
                    Product = new Product { Id = 101, Name = "Laptop", Price = 4500 },
                    Quantity = 1
                },
                new CartItem
                {
                    Id = 2,
                    UserId = 1,
                    ProductId = 102,
                    Product = new Product { Id = 102, Name = "Myszka bezprzewodowa", Price = 150 },
                    Quantity = 2
                }
            };
        }

        [HttpGet]
        public IActionResult Index()
        {
            var orderSummary = new OrderSummary
            {
                CartItems = _cartItems,
                PaymentMethods = new List<string> { "Karta kredytowa", "PayPal", "Przelew bankowy" }
            };

            return View(orderSummary);
        }

        [HttpPost]
        public IActionResult ProcessPayment(string paymentMethod)
        {
            if (string.IsNullOrEmpty(paymentMethod))
            {
                TempData["Error"] = "Wybierz metodê p³atnoœci.";
                return RedirectToAction("Index");
            }

            TempData["PaymentSuccess"] = $"P³atnoœæ metod¹ {paymentMethod} zosta³a pomyœlnie przetworzona.";
            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            ViewBag.Message = TempData["PaymentSuccess"];
            return View();
        }
    }
}
