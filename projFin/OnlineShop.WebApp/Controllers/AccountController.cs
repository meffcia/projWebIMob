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
                ModelState.AddModelError("", "Nieprawid³owy email lub has³o");
                return View(userLoginDto);
            }

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            if (result?.Data is string userId)
            {
                HttpContext.Session.SetString("userId", userId);
                HttpContext.Session.SetString("token", result.Data);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Wyst¹pi³ nieoczekiwany b³¹d.");
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
                ModelState.AddModelError("", "Has³a musz¹ byæ identyczne.");
                return View(userRegisterDto);
            }

            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/auth/register", userRegisterDto);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Rejestracja zakoñczona sukcesem! Zapraszamy do logowania.";
                return RedirectToAction("Register"); // Przekierowanie z komunikatem
            }

            var errorResponse = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Rejestracja nie powiod³a siê. Szczegó³y: {errorResponse}");
            return View(userRegisterDto);
        }

    }
}
