using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineShop.Shared;
using OnlineShop.Shared.Models;

namespace OnlineShop.WebApp.Controllers
{
    public class CheckoutController : BaseController
    {
        private readonly HttpClient _httpClient;
        private readonly string _cartApiBaseUrl;

        public CheckoutController(IHttpClientFactory httpClientFactory, string apiBaseUrl)
        {
            _httpClient = httpClientFactory.CreateClient();
            _cartApiBaseUrl = $"{apiBaseUrl}/cart";
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                int userId = GetUserIdFromSession();
                var cartResponse = await _httpClient.GetStringAsync($"{_cartApiBaseUrl}/{userId}");
                var cartData = JsonConvert.DeserializeObject<ServiceResponse<List<CartItem>>>(cartResponse);

                if (cartData?.Data == null || !cartData.Success || !cartData.Data.Any())
                {
                    TempData["ErrorMessage"] = "Koszyk jest pusty. Nie można przejść do realizacji zamówienia.";
                    return RedirectToAction("Index", "Cart");
                }

                var orderSummary = new OrderSummary
                {
                    CartItems = cartData.Data,
                    PaymentMethods = new List<string> { "CreditCard", "PayPal", "BankTransfer" }
                };

                return View(orderSummary);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Cart");
            }
        }

        public async Task<IActionResult> Summary()
        {
            try
            {
                int userId = GetUserIdFromSession();
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

                ViewBag.TotalAmount = orderSummary.CartItems.Sum(item => item.Quantity * item.Product.Price);

                return View(orderSummary);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Cart");
            }
        }
    }
}