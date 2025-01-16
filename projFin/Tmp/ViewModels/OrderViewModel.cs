using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Services.OrderService;
using OnlineShop.Shared.Services.ProductService;
using OnlineShop.MauiClient.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace OnlineShop.MauiClient.ViewModels
{
    public partial class OrderViewModel : ObservableObject, IInitializableViewModel
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly AuthStateProvider _authStateProvider;
        public Visibility AdminVisibility => isAdmin ? Visibility.Visible : Visibility.Collapsed;
        private bool isAdmin;
        [ObservableProperty]
        private string userName;

        public ObservableCollection<OrderToShow> Orders { get; set; } = new ObservableCollection<OrderToShow>();

        public OrderViewModel(IOrderService orderService, AuthStateProvider authStateProvider, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
            _authStateProvider = authStateProvider;
            isAdmin = true;//_authStateProvider.IsUserAdmin();
            UserName = "username";//_authStateProvider.GetUsername();
            var userId = 1;//_authStateProvider.GetUserIdFromToken();
            if (userId != null)
            {
                LoadOrdersAsync(userId);
            }
        }

        public async void LoadOrdersAsync(int userId)
        {
            Orders.Clear();
            var response = await _orderService.GetAllOrdersAsync(userId, null, null, false);
            if (response.Success)
            {
                var responseOrders = response.Data;
                foreach (var order in responseOrders)
                {
                    var productsString = "";
                    var price = 0;
                    foreach(var product in order.OrderItems)
                    {
                        var productResponse = await _productService.GetProductByIdAsync(product.ProductId);
                        productsString = productsString + product.Quantity + " x " + product.UnitPrice + " " + productResponse.Data.Name + "\n" ;
                        price = price + (int)product.UnitPrice*product.Quantity;
                    }
                    Console.WriteLine("orderId = " + productsString);
                    Orders.Add(new OrderToShow
                    {
                        Id = order.Id,
                        ProductString = productsString,
                        Price = price
                    });
                }
            }
            else
            {
                // Obsługa błędów, wyświetlanie komunikatu błędu
            }
        }

        [RelayCommand]
        private void LogOut()
        {
            // _authStateProvider.LogOut();
            isAdmin = false;
        }

        public async void OnViewShown()
        {
            var userId = await _authStateProvider.GetUserIdFromTokenAsync();
            if (userId != null)
            {
                LoadOrdersAsync(userId.Value);
            }
        }
    }

    public class OrderToShow
    {
        public int Id { get; set; } 
        public string ProductString { get; set; } 
        public int Price { get; set; } 
    }
}
