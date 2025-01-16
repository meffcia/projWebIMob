using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineShop.Shared.Auth;
using OnlineShop.Shared.Services.AuthService;
//using OnlineShop.Client.Services;

namespace OnlineShop.Client.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authenticationService;
        //private readonly AuthStateProvider _authStateProvider;
        //private readonly LocalStorageService _localStorageService;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string errorMessage;

        public LoginViewModel(IAuthService authenticationService)//, AuthStateProvider authStateProvider, LocalStorageService localStorageService)
        {
            _authenticationService = authenticationService;
            //_authStateProvider = authStateProvider;
            //_localStorageService = localStorageService;
        }

        [RelayCommand]
        private async Task LogIn()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Wprowadź email i hasło.";
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
                //await _localStorageService.SetItemAsync("authToken", response.Data);
                //_ = await _authStateProvider.GetAuthenticationStateAsync();
                ErrorMessage = "Logowanie pomyślne.";
            }
            else
            {
                ErrorMessage = "Niepowodzenie: " + response.Message;
            }
        }
    }
}
