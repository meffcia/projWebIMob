using Microsoft.Maui.Controls;
using OnlineShop.MauiClient.ViewModels;

namespace OnlineShop.MauiClient
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
