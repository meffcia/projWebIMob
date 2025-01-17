using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using OnlineShop.Client.Services;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.Services.CartService;
using OnlineShop.Shared.Services.OrderService;
using OnlineShop.Shared.Services.ProductService;

namespace OnlineShop.Client.ViewModels
{
    public class CartViewModel : ObservableObject
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
        }
    }
}
