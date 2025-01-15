using System.IO;

namespace OnlineShop.MauiClient.Services
{
    public class LocalStorageService
    {
        private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "authToken.txt");

        public Task SetItemAsync(string key, string value)
        {
            File.WriteAllText(_filePath, value);
            return Task.CompletedTask;
        }

        public Task<string?> GetItemAsync(string key)
        {
            if (File.Exists(_filePath))
            {
                return Task.FromResult(File.ReadAllText(_filePath));
            }
            return Task.FromResult<string?>(null);
        }

        public Task RemoveItemAsync(string key)
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
            return Task.CompletedTask;
        }
    }
}