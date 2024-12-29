using Microsoft.Maui.Controls;
using OnlineShop.MauiApp.ViewModels;

namespace OnlineShop.MauiApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
