using OnlineShop.MauiClient.ViewModels;

namespace OnlineShop.MauiClient.Views.LoginView
{
    public partial class LoginView : ContentPage
    {
        public LoginView(LoginViewModel loginViewModel)
        {
            BindingContext = loginViewModel;
            InitializeComponent();
        }
    }
}