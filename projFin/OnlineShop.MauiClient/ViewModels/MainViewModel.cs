using System;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Shared.Services.AuthService;
using OnlineShop.MauiClient.Services;
using OnlineShop.MauiClient.Views;
using OnlineShop.MauiClient.Views.LoginView;
// using OnlineShop.MauiClient.Views.CartView;
using OnlineShop.MauiClient.Views.ProductView;
// using OnlineShop.MauiClient.Views.OrderView;

namespace OnlineShop.MauiClient.ViewModels
{
    public partial class MainViewModel : ObservableObject, IMainViewModel, IAuthStateObserver
    {
        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private bool hasError;

        [ObservableProperty]
        private bool isLoggedIn;
        
        [ObservableProperty]
        private ContentPage homeView;

        [ObservableProperty]
        private ContentPage myAccountView;

        [ObservableProperty]
        private ContentPage myCartView;

        [ObservableProperty]
        private ContentPage productListView;

        [ObservableProperty]
        private double loadingRotation;

        private readonly IServiceProvider _serviceProvider;
        private readonly AuthStateProvider _authStateProvider;

        private ContentPage _selectedTab;
        public ContentPage SelectedTab
        {
            get => _selectedTab;
            set
            {
                if (_selectedTab != value)
                {
                    _selectedTab = value;
                    OnPropertyChanged(nameof(SelectedTab));

                    // if (_selectedTab?.DataContext is IInitializableViewModel initializableViewModel)
                    // {
                    //     initializableViewModel.OnViewShown();
                    // }
                }
            }
        }

        public MainViewModel(IServiceProvider serviceProvider, AuthStateProvider authStateProvider)
        {
            _serviceProvider = serviceProvider;
            _authStateProvider = authStateProvider;

            _authStateProvider.RegisterObserver(this);

            HomeView = _serviceProvider.GetRequiredService<ProductView>();
            MyAccountView = _serviceProvider.GetRequiredService<LoginView>();
            // MyCartView = _serviceProvider.GetRequiredService<CartView>();

            IsLoggedIn = true;//_authStateProvider.IsUserAuthenticatedAsync();
            SelectedTab = HomeView;
        }

        public void NavigateToRegisterView()
        {
            MyAccountView = _serviceProvider.GetRequiredService<RegisterView>();
        }

        [RelayCommand]
        public async Task NavigateToHome()
        {
            await Shell.Current.GoToAsync("//Home");
        }

        [RelayCommand]
        public async Task NavigateToMyAccount()
        {
            await Shell.Current.GoToAsync("//MyAccount");
        }

        [RelayCommand]
        public async Task NavigateToCart()
        {
            // await Shell.Current.GoToAsync("//Cart");
        }
        
        public void OnAuthenticationStateChanged()
        {
            // IsLoggedIn = _authStateProvider.IsUserAuthenticated();
            // if (isLoggedIn)
            // {
            //     MyAccountView = _serviceProvider.GetRequiredService<OrderView>();
            // }
            // else
            // {
            //     MyAccountView = _serviceProvider.GetRequiredService<LoginView>();
            // }
        }
    }
}
