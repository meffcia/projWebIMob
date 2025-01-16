using OnlineShop.Client.ViewModels;

namespace OnlineShop.Client.Views.LoginView
{
    public partial class LoginView : ContentPage
    {
        public LoginView(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            BindingContext = loginViewModel;
        }
    }
}