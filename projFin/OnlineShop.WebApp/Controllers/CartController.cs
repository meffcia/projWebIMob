using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineShop.Shared;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.WebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7077/api/cart"; // Adres API koszyka

        public CartController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // Wy�wietlenie zawarto�ci koszyka u�ytkownika
        public async Task<IActionResult> Index()
        {
            int userId = 1; // Przyk�adowe ID u�ytkownika
            try
            {
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
                ViewBag.ErrorMessage = $"Wyst�pi� b��d: {ex.Message}";
                return View(Enumerable.Empty<CartItem>());
            }
        }

        // Dodanie produktu do koszyka
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int productId, int quantity, string returnUrl = "/product/index")
        {
            int userId = 1; // Przyk�adowe ID u�ytkownika
            var cartItemDto = new AddCartItemDto
            {
                ProductId = productId,
                Quantity = quantity
            };

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(cartItemDto), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_apiBaseUrl}/{userId}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Dodano do koszyka, teraz zdecydujemy, czy zostawi� u�ytkownika na stronie produkt�w, czy przekierowa� do koszyka
                    if (returnUrl.Contains("cart"))
                    {
                        // Przekierowanie do koszyka
                        return RedirectToAction("Index", "Cart");
                    }
                    else
                    {
                        // Pozostajemy na stronie produkt�w
                        TempData["SuccessMessage"] = "Produkt zosta� dodany do koszyka!";
                        return Redirect(returnUrl); // Wr�cimy na stron�, z kt�rej przyszed� u�ytkownik
                    }
                }

                TempData["ErrorMessage"] = "Nie uda�o si� doda� produktu do koszyka.";
                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Wyst�pi� b��d: {ex.Message}";
                return Redirect(returnUrl);
            }
        }

        // Usuni�cie produktu z koszyka (przekazywanie ilo�ci do usuni�cia)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(int productId, int quantity)
        {
            int userId = 1; // Przyk�adowe ID u�ytkownika

            try
            {
                // Wysy�amy zapytanie o usuni�cie odpowiedniej ilo�ci produktu z koszyka
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{userId}/{productId}?quantity={quantity}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Koszyk zaktualizowany.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Nie uda�o si� zaktualizowa� koszyka.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Wyst�pi� b��d: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // Opr�nienie koszyka
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearCart()
        {
            int userId = 1; // Przyk�adowe ID u�ytkownika
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{userId}/clear");

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Koszyk zosta� opr�niony.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Nie uda�o si� opr�ni� koszyka.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Wyst�pi� b��d: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
