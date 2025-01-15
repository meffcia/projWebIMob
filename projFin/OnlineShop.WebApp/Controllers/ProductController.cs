using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.Auth;

namespace OnlineShop.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly List<Product> _products;

        // Konstruktor, który inicjalizuje przykładową listę produktów
        public ProductController()
        {
            // Przykładowa lista produktów
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Description = "Nowoczesny laptop", Price = 3000, Stock = 10, CategoryId = 1 },
                new Product { Id = 2, Name = "Smartphone", Description = "Smartfon z najlepszymi funkcjami", Price = 2000, Stock = 15, CategoryId = 2 },
                new Product { Id = 3, Name = "Headphones", Description = "Słuchawki bezprzewodowe", Price = 500, Stock = 30, CategoryId = 3 },
                new Product { Id = 4, Name = "Laptop", Description = "Nowoczesny laptop", Price = 3000, Stock = 10, CategoryId = 1 },
                new Product { Id = 5, Name = "Smartphone", Description = "Smartfon z najlepszymi funkcjami", Price = 2000, Stock = 15, CategoryId = 2 },
                new Product { Id = 6, Name = "Headphones", Description = "Słuchawki bezprzewodowe", Price = 500, Stock = 30, CategoryId = 3 }

            };
        }

        // Widok wszystkich produktów
        public IActionResult Index()
        {
            return View(_products); // Przekazujemy listę produktów do widoku
        }

        // Widok szczegółów jednego produktu
        public IActionResult Details(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
