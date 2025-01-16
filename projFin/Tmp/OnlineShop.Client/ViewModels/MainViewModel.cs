using System;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Shared.Services.AuthService;
using OnlineShop.Client.Services;
using OnlineShop.Client.Views;
using OnlineShop.Client.Views.LoginView;
// using OnlineShop.Client.Views.CartView;
using OnlineShop.Client.Views.ProductView;
// using OnlineShop.Client.Views.OrderView;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OnlineShop.Client.ViewModels
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

        private bool _isDrawerOpen;
        public bool IsDrawerOpen
        {
            get => _isDrawerOpen;
            set
            {
                _isDrawerOpen = value;
                OnPropertyChanged();
            }
        }

        // Metoda do otwierania/kończenia drawer
        public void ToggleDrawer()
        {
            IsDrawerOpen = !IsDrawerOpen;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
