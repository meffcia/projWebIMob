using OnlineShop.MauiClient.ViewModels;

namespace OnlineShop.MauiClient.Views.CartView
{
    public partial class CartView : ContentPage
    {
        public CartView(CartViewModel cartViewModel)
        {
            BindingContext = cartViewModel;
            InitializeComponent();
        }
    }
}