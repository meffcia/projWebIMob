using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineShop.Client.Services;
using OnlineShop.Client.Views.LoginView;
using OnlineShop.Shared.Auth;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.Services.AuthService;
using OnlineShop.Shared.Services.ProductService;
//using OnlineShop.Client.Services;

namespace OnlineShop.Client.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly AuthStateProvider _authStateProvider;
        private readonly IAuthService _authenticationService;
        //private readonly LocalStorageService _localStorageService;
        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string errorMessage;

        public LoginViewModel(AuthStateProvider authStateProvider, IAuthService authenticationService)//, AuthStateProvider authStateProvider, LocalStorageService localStorageService)
        {
            _authStateProvider = authStateProvider;
            _authenticationService = authenticationService;
        }

        [RelayCommand]
        private async Task LogIn()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                //ErrorMessage = "Wprowadź email i hasło.";
                return;
            }

            var userLoginDto = new UserLoginDto
            {
                Email = Email,
                Password = Password
            };

            var response = await _authenticationService.Login(userLoginDto);
            if (response.Success)
            {
                var jwt = response.Data;
                var result = await _authStateProvider.SaveJwtTokenAsync(jwt);

                if (result)
                {
                    var appShell = (AppShell)App.Current.MainPage;
                    appShell.ShowMainTabs();
                }
                else
                {
                    // Obsłuż błąd logowania
                }
            }
            else
            {
                //ErrorMessage = "Niepowodzenie: " + response.Message;
            }
        }

        [RelayCommand]
        private async Task NavigateToRegister()
        {
            await Shell.Current.GoToAsync(nameof(RegisterView));
        }
    }
}
