using OnlineShop.MauiClient.ViewModels;

namespace OnlineShop.MauiClient
{
    public partial class MainPage : ContentPage
    {
        public MainPage(IMainViewModel mainViewModel)
        {
            InitializeComponent();
            BindingContext = mainViewModel;
        }
    }
}
