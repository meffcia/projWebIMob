using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Client.Services
{
    public class SecureStorageService
    {
        public async Task<string?> GetAsync(string key)
        {
            // Twoja platforma może dostarczyć różne mechanizmy przechowywania (np. lokalne pliki, baza danych)
            // Przykład: lokalne pliki
            try
            {
                // Odczytanie danych z lokalnego pliku (skonfigurowanego dla Twojej platformy).
                var filePath = Path.Combine(FileSystem.AppDataDirectory, key);
                if (File.Exists(filePath))
                {
                    return await File.ReadAllTextAsync(filePath);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> RemoveAsync(string key)
        {
            // Usunięcie danych z lokalnego pliku
            try
            {
                var filePath = Path.Combine(FileSystem.AppDataDirectory, key);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SetAsync(string key, string value)
        {
            // Zapisanie danych do lokalnego pliku
            try
            {
                var filePath = Path.Combine(FileSystem.AppDataDirectory, key);
                await File.WriteAllTextAsync(filePath, value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
