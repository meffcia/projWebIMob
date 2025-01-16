using System.Threading.Tasks;

namespace OnlineShop.Client.Services
{
    public class SecureStorageService
    {
        public Task SetAsync(string key, string value)
        {
            SecureStorage.SetAsync(key, value);
            return Task.CompletedTask;
        }

        public Task<string?> GetAsync(string key)
        {
            return SecureStorage.GetAsync(key);
        }

        public Task RemoveAsync(string key)
        {
            SecureStorage.Remove(key);
            return Task.CompletedTask;
        }
    }
}