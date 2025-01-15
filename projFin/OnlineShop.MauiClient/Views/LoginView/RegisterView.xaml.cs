using System.Windows;
using OnlineShop.MauiClient.ViewModels;

namespace OnlineShop.MauiClient.Views.LoginView
{
    public partial class RegisterView : ContentPage
    {
        public RegisterView(RegisterViewModel registerViewModel)
        {
            BindingContext = registerViewModel;
            InitializeComponent();
        }
    }
}