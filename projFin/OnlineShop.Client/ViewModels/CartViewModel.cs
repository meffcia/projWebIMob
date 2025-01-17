using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Internals;
using OnlineShop.Client.Services;
using OnlineShop.Client.Views;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.Services.AuthService;
using OnlineShop.Shared.Services.CartService;
using OnlineShop.Shared.Services.OrderService;
using OnlineShop.Shared.Services.ProductService;

namespace OnlineShop.Client.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly AuthStateProvider _authStateProvider;

        public ObservableCollection<CartItem> CartItems { get; set; } = new ObservableCollection<CartItem>();

        public CartViewModel(ICartService cartService, IProductService productService, IOrderService orderService, AuthStateProvider authStateProvider)
        {
            _cartService = cartService;
            _productService = productService;
            _orderService = orderService;
            _authStateProvider = authStateProvider;
            LoadCart();
        }

        private async Task LoadCart()
        {
            CartItems.Clear();
            try
            {
                var userId = await _authStateProvider.GetUserIdFromTokenAsync();
                if (userId != null)
                {
                    var response = await _cartService.GetCartItemsAsync(userId.Value);
                    if (response.Success)
                    {
                        foreach (var cartItem in response.Data)
                        {
                            CartItems.Add(cartItem);
                        }
                    }
                    else
                    {
                        // TODO: Obsluży
                    }
                }
                else
                {
                    // TODO: Obslużyć
                }
            }
            catch (Exception ex)
            {
                // TODO: Obslużyć
            }
        }

        [RelayCommand]
        public async void AddOrder()
        {
            try
            {
                var userId = await _authStateProvider.GetUserIdFromTokenAsync();
                var newOrder = new List<OrderItemDto>();
                foreach (var item in CartItems)
                {
                    var productResponse = await _productService.GetProductByIdAsync(item.ProductId);
                    if (productResponse.Success)
                    {
                        newOrder.Add(new OrderItemDto()
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            UnitPrice = productResponse.Data.Price,
                            ProductName = productResponse.Data.Name
                        });

                    }
                }
                if (userId != null)
                {
                    var order = new CreateOrderDto()
                    {
                        UserId = userId.Value,
                        OrderItems = newOrder
                    };
                    await _orderService.AddOrderAsync(order);
                }
            }
            catch (Exception ex)
            {
                // Obsługa błędów
                Console.WriteLine($"Error creating order: {ex.Message}");
            }
        }

        [RelayCommand]
        public async void ClearCart()
        {
            var userId = await _authStateProvider.GetUserIdFromTokenAsync();
            if (userId != null)
            {
                await _cartService.ClearCartAsync(userId.Value);
            }
        }

        [RelayCommand]
        public async void NavigateToCheckout()
        {
            await Shell.Current.GoToAsync(nameof(CheckoutView));
        }
    }
}
