using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.Auth;

namespace OnlineShop.WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly List<CartItem> _cartItems;
        private readonly List<Product> _products;
        private readonly User _currentUser;

        public CartController()
        {
            // Przyk³adowy zalogowany u¿ytkownik
            _currentUser = new User { Id = 1, Username = "exampleUser" };

            // Przyk³adowe produkty
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop Dell XPS 13", Price = 5499.99m },
                new Product { Id = 2, Name = "Smartfon Samsung Galaxy S23", Price = 3999.50m },
                new Product { Id = 3, Name = "S³uchawki Bose QC45", Price = 1349.99m }
            };

            // Przyk³adowe dane koszyka
            _cartItems = new List<CartItem>
            {
                new CartItem { Id = 1, UserId = _currentUser.Id, User = _currentUser, ProductId = 1, Product = _products[0], Quantity = 1 },
                new CartItem { Id = 2, UserId = _currentUser.Id, User = _currentUser, ProductId = 2, Product = _products[1], Quantity = 2 }
            };
        }

        // Dodaj produkt do koszyka
        public IActionResult AddToCart(int productId, int quantity)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null) return NotFound();

            var cartItem = _cartItems.FirstOrDefault(c => c.ProductId == productId && c.UserId == _currentUser.Id);
            if (cartItem == null)
            {
                _cartItems.Add(new CartItem
                {
                    Id = _cartItems.Count + 1,
                    UserId = _currentUser.Id,
                    User = _currentUser,
                    ProductId = productId,
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            return RedirectToAction("Index");
        }

        // Wyœwietlenie zawartoœci koszyka
        public IActionResult Index()
        {
            var userCartItems = _cartItems.Where(c => c.UserId == _currentUser.Id).ToList();
            return View(userCartItems);
        }
    }
}
