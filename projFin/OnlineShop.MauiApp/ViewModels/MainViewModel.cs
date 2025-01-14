using System.Collections.ObjectModel;
using System.Windows.Input;
using OnlineShop.Shared.DTOs;

namespace OnlineShop.MauiApp.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<ProductDto> Products { get; set; }

        public ICommand AddProductCommand { get; }
        public ICommand RefreshCommand { get; }

        public MainViewModel()
        {
            // Sample data
            Products = new ObservableCollection<ProductDto>
            {
                new ProductDto { Id = 1, Name = "Laptop", Description = "Powerful laptop", Price = 1200.99m, Stock = 10, CategoryId = 1 },
                new ProductDto { Id = 2, Name = "Smartphone", Description = "Latest model smartphone", Price = 799.99m, Stock = 25, CategoryId = 2 },
                new ProductDto { Id = 3, Name = "Headphones", Description = "Noise-cancelling headphones", Price = 199.99m, Stock = 50, CategoryId = 3 }
            };

            AddProductCommand = new Command(AddProduct);
            RefreshCommand = new Command(RefreshProducts);
        }

        private void AddProduct()
        {
            // Logic for adding a new product
            Products.Add(new ProductDto
            {
                Id = Products.Count + 1,
                Name = "New Product",
                Description = "Description of the new product",
                Price = 100.00m,
                Stock = 5,
                CategoryId = 4
            });
        }

        private void RefreshProducts()
        {
            // Logic for refreshing the product list
        }
    }
}
