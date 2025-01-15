using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.Auth;

namespace OnlineShop.WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly List<CartItem> _cartItems;

        // Inicjalizujemy przyk³adowy koszyk
        public CartController()
        {
            _cartItems = new List<CartItem>();
        }

        // Dodaj produkt do koszyka
        public IActionResult AddToCart(int productId, int quantity)
        {
            var cartItem = _cartItems.FirstOrDefault(c => c.ProductId == productId);
            if (cartItem == null)
            {
                _cartItems.Add(new CartItem { ProductId = productId, Quantity = quantity });
            }
            else
            {
                cartItem.Quantity += quantity;
            }
            return RedirectToAction("Index", "Product");
        }

        // Wyœwietlenie zawartoœci koszyka
        public IActionResult Index()
        {
            return View(_cartItems);
        }
    }
}
