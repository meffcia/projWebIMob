using System.Windows;
using OnlineShop.Client.ViewModels;

namespace OnlineShop.Client.Views.LoginView
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