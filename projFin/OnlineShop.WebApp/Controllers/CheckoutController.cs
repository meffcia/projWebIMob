using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineShop.Shared;
using OnlineShop.Shared.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnlineShop.WebApp.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _cartApiBaseUrl = "https://localhost:7077/api/cart"; // Adres API koszyka

        public CheckoutController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // Wyświetlenie strony Checkout
        public async Task<IActionResult> Index()
        {
            int userId = 1; // Przykładowe ID użytkownika

            try
            {
                // Pobierz zawartość koszyka użytkownika
                var cartResponse = await _httpClient.GetStringAsync($"{_cartApiBaseUrl}/{userId}");
                var cartData = JsonConvert.DeserializeObject<ServiceResponse<List<CartItem>>>(cartResponse);

                if (cartData?.Data == null || !cartData.Success || !cartData.Data.Any())
                {
                    TempData["ErrorMessage"] = "Koszyk jest pusty. Nie można przejść do realizacji zamówienia.";
                    return RedirectToAction("Index", "Cart");
                }

                // Utwórz model na podstawie istniejących danych
                var orderSummary = new OrderSummary
                {
                    CartItems = cartData.Data,
                    PaymentMethods = new List<string> { "CreditCard", "PayPal", "BankTransfer" } // Możesz dostosować
                };

                return View(orderSummary);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Wystąpił błąd: {ex.Message}";
                return RedirectToAction("Index", "Cart");
            }
        }

        // Strona podsumowania
        public async Task<IActionResult> Summary()
        {
            int userId = 1; // Przykładowe ID użytkownika

            try
            {
                // Pobierz zawartość koszyka użytkownika z API
                var cartResponse = await _httpClient.GetStringAsync($"{_cartApiBaseUrl}/{userId}");
                var cartData = JsonConvert.DeserializeObject<ServiceResponse<List<CartItem>>>(cartResponse);

                if (cartData?.Data == null || !cartData.Success || !cartData.Data.Any())
                {
                    TempData["ErrorMessage"] = "Koszyk jest pusty. Nie można przejść do realizacji zamówienia.";
                    return RedirectToAction("Index", "Cart");
                }

                var orderSummary = new OrderSummary
                {
                    CartItems = cartData.Data
                };

                // Oblicz całkowitą kwotę
                var totalAmount = orderSummary.CartItems.Sum(item => item.Quantity * item.Product.Price);

                // Wstawiamy dane do widoku
                ViewBag.TotalAmount = totalAmount;

                return View(orderSummary);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Wystąpił błąd: {ex.Message}";
                return RedirectToAction("Index", "Cart");
            }
        }
    }
}
