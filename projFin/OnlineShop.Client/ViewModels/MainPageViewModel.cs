using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using OnlineShop.Shared.Services.ProductService;

namespace OnlineShop.Client.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private IProductService _productService;

        [ObservableProperty]
        private int count;

        public string WelcomeMessage => "Hello, World!";
        public string SubHeadingMessage => "Welcome to .NET Multi-platform App UI";
        public string CounterMessage => count == 1 ? $"Clicked {count} time" : $"Clicked {count} times";

        public MainPageViewModel(IProductService productService)
        {
            _productService = productService;
            LoadProducts();
        }

        [RelayCommand]
        private void IncrementCounter()
        {
            count++;
        }

        private async void LoadProducts()
        {
            // int? categoryFilter = SelectedCategoryId != 0 ? SelectedCategoryId : null;
            try
            {
                var response = await _productService.GetAllProductsAsync(null, null, null, false, null, null);//SearchQuery, categoryFilter, SortBy, Descending, CurrentPage, ItemsPerPage);

                if (response.Success)
                {
                    // MainThread.BeginInvokeOnMainThread(() =>
                    // {
                    //     Products.Clear();
                    //     foreach (var product in response.Data.Products)
                    //     {
                    //         Products.Add(new AddToCartParameters { Product = product, Quantity = 0 });
                    //     }
                    // });
                    // TotalPages = (int)Math.Ceiling((double)response.Data.TotalItems / ItemsPerPage);
                }
                else
                {
                    // ErrorMessage = response.Message;
                }
            }
            catch (Exception ex)
            {
                // ErrorMessage = "An unexpected error occurred.";
                // TODO: zwraca na razie Exception
            }
        }
    }
}
