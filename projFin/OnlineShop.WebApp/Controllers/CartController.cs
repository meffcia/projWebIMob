using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Dodaj to
using Newtonsoft.Json;
using OnlineShop.Shared;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Models;
using System.Text;

namespace OnlineShop.WebApp.Controllers
{
    public class CartController : BaseController
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public CartController(HttpClient httpClient, string apiBaseUrl, ILogger<CartController> logger)
        {
            _httpClient = httpClient;
            _apiBaseUrl = $"{apiBaseUrl}/cart";
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                int userId = GetUserIdFromSession();

                var response = await _httpClient.GetStringAsync($"{_apiBaseUrl}/{userId}");
                var cartResponse = JsonConvert.DeserializeObject<ServiceResponse<List<CartItem>>>(response);

                if (cartResponse?.Data == null || !cartResponse.Success)
                {
                    ViewBag.ErrorMessage = "Koszyk jest pusty.";
                    return View(Enumerable.Empty<CartItem>());
                }

                return View(cartResponse.Data);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Wyst¹pi³ b³¹d: {ex.Message}";
                return View(Enumerable.Empty<CartItem>());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int productId, int quantity, string returnUrl = "/product/index")
        {
            try
            {
                int userId = GetUserIdFromSession();

                var cartItemDto = new AddCartItemDto
                {
                    ProductId = productId,
                    Quantity = quantity
                };

                var content = new StringContent(JsonConvert.SerializeObject(cartItemDto), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/{userId}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Produkt zosta³ dodany do koszyka!";
                    return Redirect(returnUrl);
                }

                TempData["ErrorMessage"] = "Nie uda³o siê dodaæ produktu do koszyka.";
                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Wyst¹pi³ b³¹d: {ex.Message}";
                return Redirect(returnUrl);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(int productId, int quantity)
        {
            try
            {
                int userId = GetUserIdFromSession();

                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{userId}/{productId}?quantity={quantity}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Koszyk zaktualizowany.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Nie uda³o siê zaktualizowaæ koszyka.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Wyst¹pi³ b³¹d: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearCart()
        {
            try
            {
                int userId = GetUserIdFromSession();

                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{userId}/clear");

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Koszyk zosta³ opró¿niony.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Nie uda³o siê opró¿niæ koszyka.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Wyst¹pi³ b³¹d: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}