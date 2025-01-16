using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared;
using OnlineShop.Shared.Auth; // Dodaj odpowiedni¹ klasê
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OnlineShop.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://localhost:7077/api/auth"; // Adres bazowy API

        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync($"{ApiBaseUrl}/login", userLoginDto);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();

                    // Pobieranie userId i zapisywanie w sesji
                    var userId = result?.Data; // Zak³adaj¹c, ¿e Data to userId

                    if (userId != null)
                    {
                        HttpContext.Session.SetString("userId", userId.ToString());
                        HttpContext.Session.SetString("token", result.Data);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Nieprawid³owy email lub has³o");
                }
            }

            return View(userLoginDto);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync($"{ApiBaseUrl}/register", userRegisterDto);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Rejestracja nie powiod³a siê.");
                }
            }

            return View(userRegisterDto);
        }
    }
}
