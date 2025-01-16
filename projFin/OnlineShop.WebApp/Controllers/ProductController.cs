using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineShop.Shared;
using OnlineShop.Shared.DTOs;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        // Konstruktor z dependency injection dla HttpClient i apiBaseUrl
        public ProductController(HttpClient httpClient, string apiBaseUrl)
        {
            _httpClient = httpClient;
            _apiBaseUrl = $"{apiBaseUrl}/products"; // Dodanie endpointu do baseUrl
        }

        // Widok wszystkich produktów
        public async Task<IActionResult> Index()
        {
            try
            {
                // Pobierz dane z API
                var response = await _httpClient.GetStringAsync(_apiBaseUrl);
                var productsResponse = JsonConvert.DeserializeObject<ServiceResponse<ProductsListDto>>(response);

                if (productsResponse?.Data?.Products == null)
                {
                    ViewBag.ErrorMessage = "Brak produktów do wyświetlenia.";
                    return View(Enumerable.Empty<ProductDto>());
                }

                return View(productsResponse.Data.Products);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Wystąpił błąd: {ex.Message}";
                return View(Enumerable.Empty<ProductDto>());
            }
        }

        // Widok szczegółów jednego produktu
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{_apiBaseUrl}/{id}");
                var productResponse = JsonConvert.DeserializeObject<ServiceResponse<ProductDto>>(response);

                if (productResponse == null || !productResponse.Success)
                {
                    return NotFound("Nie znaleziono produktu.");
                }

                return View(productResponse.Data);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Wystąpił błąd: {ex.Message}";
                return View();
            }
        }

        // Widok formularza do dodania nowego produktu
        public IActionResult Create()
        {
            return View();
        }

        // Akcja do dodania nowego produktu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var content = new StringContent(JsonConvert.SerializeObject(productDto), Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(_apiBaseUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index)); // Po udanym dodaniu, przekierowanie do widoku listy
                    }

                    ViewBag.ErrorMessage = "Nie udało się dodać produktu.";
                    return View(productDto);
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"Wystąpił błąd: {ex.Message}";
                    return View(productDto);
                }
            }

            return View(productDto); // W przypadku błędów walidacji formularza
        }
    }
}
