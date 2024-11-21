using Newtonsoft.Json;
using proj4.Models;
using System.IO;

namespace proj4.Services
{
    public class FileProductService : IProductService
    {
        // Określamy pełną ścieżkę do pliku w katalogu głównym projektu
        private readonly string _filePath;

        public FileProductService()
        {
            // Używamy katalogu głównego projektu
            var projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _filePath = Path.Combine(projectDirectory, "products.json");

            // Jeśli plik nie istnieje, tworzysz go z pustą listą produktów
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(new List<IProduct>()));
            }
        }

        public async Task<ServiceReponse<List<IProduct>>> GetAllProductAsync()
        {
            try
            {
                var json = await File.ReadAllTextAsync(_filePath);
                var products = JsonConvert.DeserializeObject<List<Book>>(json) ?? new List<Book>();
                return new ServiceReponse<List<IProduct>>
                {
                    Data = products.Cast<IProduct>().ToList(),
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<List<IProduct>>
                {
                    Message = $"Error loading products: {ex.Message}",
                    Success = false
                };
            }
        }


        public async Task<ServiceReponse<IProduct>> AddProductAsync(IProduct product)
        {
            try
            {
                var json = await File.ReadAllTextAsync(_filePath);
                var products = JsonConvert.DeserializeObject<List<Book>>(json) ?? new List<Book>();

                var newProduct = new Book
                {
                    Id = products.Any() ? products.Max(p => p.Id) + 1 : 1,
                    Title = product.Title,
                    Author = product.Author,
                    Price = product.Price
                };

                products.Add(newProduct);
                await File.WriteAllTextAsync(_filePath, JsonConvert.SerializeObject(products));

                return new ServiceReponse<IProduct>
                {
                    Data = newProduct,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<IProduct>
                {
                    Message = $"Error adding product: {ex.Message}",
                    Success = false
                };
            }
        }

        public async Task<ServiceReponse<IProduct>> UpdateProductAsync(IProduct updatedProduct)
        {
            try
            {
                if (updatedProduct == null)
                {
                    return new ServiceReponse<IProduct>
                    {
                        Message = "Product data is null.",
                        Success = false
                    };
                }

                // Wczytanie istniejących produktów z pliku
                var json = await File.ReadAllTextAsync(_filePath);
                var products = JsonConvert.DeserializeObject<List<Book>>(json) ?? new List<Book>();

                // Szukamy produktu, który chcemy zaktualizować
                var existingProduct = products.FirstOrDefault(p => p.Id == updatedProduct.Id);
                if (existingProduct == null)
                {
                    return new ServiceReponse<IProduct>
                    {
                        Message = "Product not found.",
                        Success = false
                    };
                }

                // Możemy rozważyć walidację danych produktu
                if (string.IsNullOrEmpty(updatedProduct.Title) || string.IsNullOrEmpty(updatedProduct.Author) || updatedProduct.Price <= 0)
                {
                    return new ServiceReponse<IProduct>
                    {
                        Message = "Invalid product data.",
                        Success = false
                    };
                }

                // Aktualizujemy właściwości produktu
                existingProduct.Title = updatedProduct.Title;
                existingProduct.Author = updatedProduct.Author;
                existingProduct.Price = updatedProduct.Price;

                // Zapisujemy zmienione dane do pliku
                await File.WriteAllTextAsync(_filePath, JsonConvert.SerializeObject(products));

                return new ServiceReponse<IProduct>
                {
                    Data = existingProduct,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<IProduct>
                {
                    Message = $"Error updating product: {ex.Message}",
                    Success = false
                };
            }
        }



        public async Task<ServiceReponse<IProduct>> DeleteProductAsync(int productId)
        {
            try
            {
                var json = await File.ReadAllTextAsync(_filePath);
                var products = JsonConvert.DeserializeObject<List<Book>>(json) ?? new List<Book>();

                var productToDelete = products.FirstOrDefault(p => p.Id == productId);
                if (productToDelete == null)
                {
                    return new ServiceReponse<IProduct>
                    {
                        Message = "Product not found.",
                        Success = false
                    };
                }

                products.Remove(productToDelete);
                await File.WriteAllTextAsync(_filePath, JsonConvert.SerializeObject(products));

                return new ServiceReponse<IProduct>
                {
                    Data = productToDelete,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceReponse<IProduct>
                {
                    Message = $"Error deleting product: {ex.Message}",
                    Success = false
                };
            }
        }
    }
}
