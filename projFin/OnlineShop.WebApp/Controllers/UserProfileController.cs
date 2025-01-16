using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared.Auth; // Model User


namespace OnlineShop.WebApp.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://localhost:7077/api/Auth"; // Bazowy URL API

        public UserProfileController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Pobierz profil u¿ytkownika na podstawie ID
        private async Task<User> GetUserProfileAsync(string userId)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{ApiBaseUrl}/User/{userId}");
            requestMessage.Headers.Add("Authorization", $"Bearer {HttpContext.Session.GetString("token")}");

            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }
            else
            {
                HttpContext.Session.Remove("token");
                HttpContext.Session.Remove("userId");
                return null;
            }
        }

        // Akcja do wyœwietlania profilu u¿ytkownika
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userProfile = await GetUserProfileAsync(userId);
            if (userProfile == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(userProfile); // Przekazanie danych u¿ytkownika do widoku
        }

        // Akcja do edytowania danych u¿ytkownika
        [HttpGet]
        public IActionResult Edit()
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Tutaj mo¿na zaimplementowaæ pobieranie danych do edycji
            return View();
        }

        // Akcja do zapisania zaktualizowanych danych u¿ytkownika
        [HttpPost]
        public async Task<IActionResult> Edit(User model)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetString("userId");
                if (userId == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // Logika zapisu danych u¿ytkownika (PUT request do API)
                var requestMessage = new HttpRequestMessage(HttpMethod.Put, $"{ApiBaseUrl}/User/{userId}");
                requestMessage.Headers.Add("Authorization", $"Bearer {HttpContext.Session.GetString("token")}");
                requestMessage.Content = JsonContent.Create(model);

                var response = await _httpClient.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Nie uda³o siê zaktualizowaæ danych.");
                }
            }

            return View(model);
        }
    }
}
