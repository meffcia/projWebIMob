using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Services.ProductService;

namespace OnlineShop.Client.ViewModels
{
    public partial class EditProductViewModel : ObservableObject
    {
        private IProductService _serviceProduct;
        public ProductDto OriginalProduct { get; }
        public ProductDto Product { get; }
        public bool IsEditing { get; }

        public EditProductViewModel(IProductService productService, ProductDto product, bool isEditing)
        {
            _serviceProduct = productService;
            if (isEditing)
            {
                OriginalProduct = product;
                Product = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    CategoryId = product.CategoryId
                };
            }
            else
            {
                Product = product;
            }
            IsEditing = isEditing;
        }

        [RelayCommand]
        public async void Save()
        {
            if (IsEditing)
            {
                var updateProduct = new UpdateProductDto();
        
                if (OriginalProduct.Name != Product.Name) updateProduct.Name = Product.Name;
                if (OriginalProduct.Description != Product.Description) updateProduct.Description = Product.Description;
                if (OriginalProduct.Price != Product.Price) updateProduct.Price = Product.Price;
                if (OriginalProduct.Stock != Product.Stock) updateProduct.Stock = Product.Stock;
                if (OriginalProduct.CategoryId != Product.CategoryId) updateProduct.CategoryId = Product.CategoryId;

                try
                {
                    var response = await _serviceProduct.UpdateProductAsync(Product.Id, updateProduct);

                    if (response.Success)
                    {
                        if (OriginalProduct.Name != Product.Name) OriginalProduct.Name = Product.Name;
                        if (OriginalProduct.Description != Product.Description) OriginalProduct.Description = Product.Description;
                        if (OriginalProduct.Price != Product.Price) OriginalProduct.Price = Product.Price;
                        if (OriginalProduct.Stock != Product.Stock) OriginalProduct.Stock = Product.Stock;
                        if (OriginalProduct.CategoryId != Product.CategoryId) OriginalProduct.CategoryId = Product.CategoryId;
                        CloseWindow();
                    }
                    else
                    {
                        // MessageBox.Show($"Unable to save changes.: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    // MessageBox.Show($"Unable to save changes.: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                var createProduct = new CreateProductDto()
                {
                    Name = Product.Name,
                    Description = Product.Description,
                    Price = Product.Price,
                    Stock = Product.Stock,
                    CategoryId = Product.CategoryId
                };

                var response = await _serviceProduct.AddProductAsync(createProduct);

                if (response.Success)
                {
                    CloseWindow();
                }
                else
                {
                    // MessageBox.Show($"Unable to add product: {response.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CloseWindow()
        {
            // Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)?.Close();
        }
    }
}
