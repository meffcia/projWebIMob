using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineShop.Client.Services;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.Services.CartService;
using OnlineShop.Shared.Services.ProductService;

namespace OnlineShop.Client.ViewModels
{
    public partial class ProductViewModel : ObservableObject
    {
        private readonly AuthStateProvider _authStateProvider;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        public ObservableCollection<ProductDto> Products { get; set; } = new ObservableCollection<ProductDto>();

        public ProductViewModel(AuthStateProvider authStateProvider, IProductService productService, ICartService cartService)
        {
            _authStateProvider = authStateProvider;
            _productService = productService;
            _cartService = cartService;
            LoadProducts();
        }


        private async Task LoadProducts()
        {
            try
            {
                var response = await _productService.GetAllProductsAsync(null, null, null, false, null, null);
                Products.Clear();

                if (response.Success)
                {
                    foreach (var product in response.Data.Products)
                    {
                        Products.Add(product);
                    }
                }
                else
                {
                    // TODO: Inform user 
                }
            }
            catch (Exception ex)
            {
                // TODO: Inform user 
            }
        }

        [RelayCommand]
        public async void AddToCart(ProductDto product)
        {
            var userId = await _authStateProvider.GetUserIdFromTokenAsync();

            if (userId != null)
            {
                var addCartItemDto = new AddCartItemDto { ProductId = product.Id, Quantity = 1, UserId = userId.Value };
                var response = await _cartService.AddToCartAsync(userId.Value, addCartItemDto);
            }
            else
            {
                // TODO: Inform user 
            }
        }

        [RelayCommand]
        public async void Details(ProductDto product)
        {
            var userId = await _authStateProvider.GetUserIdFromTokenAsync();

            if (userId != null)
            {
                var addCartItemDto = new AddCartItemDto { ProductId = product.Id, Quantity = 1, UserId = userId.Value };
                var response = await _cartService.AddToCartAsync(userId.Value, addCartItemDto);
            }
            else
            {
                // TODO: Inform user 
            }
        }
    }
}
