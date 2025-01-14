using OnlineShop.Shared.Auth;
using System.Net.Http.Json;

namespace OnlineShop.Shared.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(string newPassword)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Auth/ChangePassword", newPassword);
            var content = await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            return content;
        }

        public async Task<ServiceResponse<string>> Login(UserLoginDto userLogin)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Auth/Login", userLogin);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<string>>()
                    ?? new ServiceResponse<string> { Success = false, Message = "Failed to log in"};
        }

        public async Task<ServiceResponse<int>> Register(UserRegisterDto userRegister)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Auth/Register", userRegister);
            var content = await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            return content;
        }
    }
}