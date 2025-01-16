using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared.Auth;
using OnlineShop.Shared;

namespace OnlineShop.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public AccountController(HttpClient httpClient, string apiBaseUrl)
        {
            _httpClient = httpClient;
            _apiBaseUrl = apiBaseUrl;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
                return View(userLoginDto);

            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/auth/login", userLoginDto);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Nieprawid�owy email lub has�o");
                return View(userLoginDto);
            }

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            if (result?.Data is string userId)
            {
                HttpContext.Session.SetString("userId", userId);
                HttpContext.Session.SetString("token", result.Data);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Wyst�pi� nieoczekiwany b��d.");
            return View(userLoginDto);
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
                return View(userRegisterDto);

            if (userRegisterDto.Password != userRegisterDto.ConfirmPassword)
            {
                ModelState.AddModelError("", "Has�a musz� by� identyczne.");
                return View(userRegisterDto);
            }

            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/auth/register", userRegisterDto);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Rejestracja zako�czona sukcesem! Zapraszamy do logowania.";
                return RedirectToAction("Register"); // Przekierowanie z komunikatem
            }

            var errorResponse = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Rejestracja nie powiod�a si�. Szczeg�y: {errorResponse}");
            return View(userRegisterDto);
        }

    }
}
