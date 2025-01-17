using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineShop.Client.Services;
using OnlineShop.Client.Views;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.Services.CartService;

namespace OnlineShop.Client.ViewModels
{
    public partial class ProductDetailsViewModel : ObservableObject
    {
        private readonly AuthStateProvider _authStateProvider;
        private readonly ICartService _cartService;
        public ProductDetailsViewModel(AuthStateProvider authStateProvider, ICartService cartService)
        {
            _authStateProvider = authStateProvider;
            _cartService = cartService;
        }

        [RelayCommand]
        public async Task NavigateToProducts()
        {
            await Shell.Current.GoToAsync(nameof(ProductView));
        }

        [RelayCommand]
        public async Task AddToCart(ProductDto product)
        {
            try
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
            catch (Exception ex)
            {

            }
        }

        [RelayCommand]
        public async Task Details()
        {

        }
    }
}