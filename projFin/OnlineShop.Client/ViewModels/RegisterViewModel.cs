using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineShop.Shared.Auth;
using OnlineShop.Shared.Services.AuthService;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Client.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        //[ObservableProperty]
        //private string email;

        //[ObservableProperty]
        //private string userName;

        //[ObservableProperty]
        //private string password;

        //[ObservableProperty]
        //private string confirmPassword;

        //[ObservableProperty]
        //private string errorMessage;

        public RegisterViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        //[RelayCommand]
        //private async void Register()
        //{
        //    if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
        //    {
        //        ErrorMessage = "Pola nie mogą być puste.";
        //        return;
        //    }
        //    if (Password != ConfirmPassword)
        //    {
        //        ErrorMessage = "Pola 'Password' i 'ConfirmPassword' muszą być identyczne.";
        //        return;
        //    }

        //    var userRegisterDto = new UserRegisterDto
        //    {
        //        Email = Email,
        //        UserName = UserName,
        //        Password = Password,
        //        ConfirmPassword = ConfirmPassword
        //    };

        //    var validationResults = new List<ValidationResult>();
        //    var validationContext = new ValidationContext(userRegisterDto);

        //    if (!Validator.TryValidateObject(userRegisterDto, validationContext, validationResults, true))
        //    {
        //        ErrorMessage = string.Join("\n", validationResults.Select(v => v.ErrorMessage));
        //        return;
        //    }

        //    try
        //    {
        //        await _authService.Register(userRegisterDto);
        //        ErrorMessage = "Rejestracja przebiegła pomyślnie.";
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMessage = $"Błąd rejestracji: {ex.Message}";
        //    }
        //}
    }
}
