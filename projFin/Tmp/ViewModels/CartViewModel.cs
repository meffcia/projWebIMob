using OnlineShop.Shared.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineShop.MauiClient.ViewModels;
using OnlineShop.MauiClient.Services;
using System.Windows;
using OnlineShop.Shared.Services.CartService;
using OnlineShop.Shared.Services.OrderService;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Services.ProductService;

namespace OnlineShop.MauiClient.ViewModels
{
    public partial class CartViewModel : ObservableObject, IInitializableViewModel
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly AuthStateProvider _authStateProvider;

        public ObservableCollection<CartItem> CartItems { get; set; } = new ObservableCollection<CartItem>();

        [ObservableProperty]
        private decimal _totalPrice;
        [ObservableProperty]
        private bool isUserLoggedIn;

        [RelayCommand]
        private async void RemoveFromCart(int productId)
        {
            var userId = await _authStateProvider.GetUserIdFromTokenAsync();
            if (userId != null)
            {
                _cartService.RemoveFromCartAsync(userId.Value, productId);
            }
            RefreshCartItems();
        }

        public CartViewModel(ICartService cartService, IProductService productService, IOrderService orderService, AuthStateProvider authStateProvider)
        {
            _cartService = cartService;
            _productService = productService;
            _orderService = orderService;
            _authStateProvider = authStateProvider;

            CheckUserLoggedInStatus();
            RefreshCartItems();
        }
        
        public void OnViewShown()
        {
            RefreshCartItems();
        }

        [RelayCommand]
        private async void ClearCart()
        {
            var userId = await _authStateProvider.GetUserIdFromTokenAsync();
            if (userId != null)
            {
                await _cartService.ClearCartAsync(userId.Value);
            }
            RefreshCartItems();
        }

        [RelayCommand]
        private async void CreateOrder()
        {
            var userId = await _authStateProvider.GetUserIdFromTokenAsync();
            var cartItems = new List<OrderItemDto>();
            foreach (var item in CartItems)
            {
                var productResponse = await _productService.GetProductByIdAsync(item.ProductId);
                if (productResponse.Success)
                {
                    cartItems.Add(new OrderItemDto()
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
                    OrderItems = cartItems
                };
                await _orderService.AddOrderAsync(order);
            }
            RefreshCartItems();
        }

        private async void CheckUserLoggedInStatus()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            IsUserLoggedIn = true;//authState.User.Identity?.IsAuthenticated ?? false;
        }

        private async void RefreshCartItems()
        {
            CartItems.Clear();
            var userId = await _authStateProvider.GetUserIdFromTokenAsync();
            if (userId != null)
            {
                var response = await _cartService.GetCartItemsAsync(userId.Value);
                if (response.Success)
                {
                    var items = response.Data;
                    if (items == null)
                    {
                        return;
                    }
                    foreach (var item in items)
                    {
                        CartItems.Add(item);
                    }

                    TotalPrice = GetTotalPrice(items);
                }
                else
                {
                    return;
                }
            }
        }

        private decimal GetTotalPrice(List<CartItem> items)
        {
            if (items == null || !items.Any())
            {
                return 0;
            }

            return items.Sum(item => item.Product.Price * item.Quantity);
        }
    }
}
