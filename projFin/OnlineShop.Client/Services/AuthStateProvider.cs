using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;

namespace OnlineShop.Client.Services
{
    public partial class AuthStateProvider : ObservableObject
    {
        private readonly SecureStorageService _secureStorageService;
        private readonly HttpClient _httpClient;
        private readonly List<IAuthStateObserver> _observers = new();

        public AuthStateProvider(SecureStorageService secureStorageService, HttpClient httpClient)
        {
            _secureStorageService = secureStorageService;
            _httpClient = httpClient;
        }

        public void RegisterObserver(IAuthStateObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        private void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.OnAuthenticationStateChanged();
            }
        }

        public async Task<bool> LogOutAsync()
        {
            try
            {
                await _secureStorageService.RemoveAsync("authToken");
                _httpClient.DefaultRequestHeaders.Authorization = null;

                NotifyObservers();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during logout: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> IsUserAuthenticatedAsync()
        {
            var authToken = await _secureStorageService.GetAsync("authToken");
            return !string.IsNullOrEmpty(authToken) && !IsTokenExpired(authToken);
        }

        private bool IsTokenExpired(string authToken)
        {
            try
            {
                var claims = ParseClaimsFromJWT(authToken);
                var expirationClaim = claims.FirstOrDefault(c => c.Type == "exp");

                if (expirationClaim == null)
                    return true;

                var expirationDateUnix = long.Parse(expirationClaim.Value);
                var expirationDate = DateTimeOffset.FromUnixTimeSeconds(expirationDateUnix).DateTime;

                return expirationDate <= DateTime.UtcNow;
            }
            catch
            {
                return true;
            }
        }

        public async Task<ClaimsPrincipal> GetAuthenticationStateAsync()
        {
            var authToken = await _secureStorageService.GetAsync("authToken");
            var identity = new ClaimsIdentity();

            _httpClient.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(authToken))
            {
                try
                {
                    identity = new ClaimsIdentity(ParseClaimsFromJWT(authToken), "jwt");
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", authToken);
                }
                catch
                {
                    await _secureStorageService.RemoveAsync("authToken");
                    identity = new ClaimsIdentity();
                }
            }

            NotifyObservers();
            return new ClaimsPrincipal(identity);
        }

        private IEnumerable<Claim> ParseClaimsFromJWT(string authToken)
        {
            var payload = authToken.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private byte[] ParseBase64WithoutPadding(string payload)
        {
            switch (payload.Length % 4)
            {
                case 2:
                    payload += "==";
                    break;
                case 3:
                    payload += "=";
                    break;
            }
            return Convert.FromBase64String(payload);
        }

        public async Task<bool> IsUserAdminAsync()
        {
            var authToken = await _secureStorageService.GetAsync("authToken");

            if (string.IsNullOrEmpty(authToken) || IsTokenExpired(authToken))
                return false;

            var claims = ParseClaimsFromJWT(authToken);
            return claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
        }

        public async Task<int?> GetUserIdFromTokenAsync()
        {
            var authToken = await _secureStorageService.GetAsync("authToken");

            if (string.IsNullOrEmpty(authToken) || IsTokenExpired(authToken))
                return null;

            var claims = ParseClaimsFromJWT(authToken);
            var userIdClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId) ? userId : null;
        }

        public async Task<string?> GetUsernameAsync()
        {
            var authToken = await _secureStorageService.GetAsync("authToken");

            if (string.IsNullOrEmpty(authToken) || IsTokenExpired(authToken))
                return null;

            var claims = ParseClaimsFromJWT(authToken);
            var usernameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            return usernameClaim?.Value;
        }
    }

    public interface IAuthStateObserver
    {
        void OnAuthenticationStateChanged();
    }
}
