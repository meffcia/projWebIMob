using Newtonsoft.Json;
using proj4.Models;

namespace proj4.Services
{
    public class FileProductService : IProductService
    {
        private readonly string _filePath = "products.json";

        public FileProductService()
        {
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
                var json = await File.ReadAllTextAsync(_filePath);
                var products = JsonConvert.DeserializeObject<List<Book>>(json) ?? new List<Book>();

                var existingProduct = products.FirstOrDefault(p => p.Id == updatedProduct.Id);
                if (existingProduct == null)
                {
                    return new ServiceReponse<IProduct>
                    {
                        Message = "Product not found.",
                        Success = false
                    };
                }

                // Aktualizacja właściwości produktu
                existingProduct.Title = updatedProduct.Title;
                existingProduct.Author = updatedProduct.Author;
                existingProduct.Price = updatedProduct.Price;

                // Zapisanie zmian do pliku
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
    }
}
