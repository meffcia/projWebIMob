using OnlineShop.MauiClient.ViewModels;

namespace OnlineShop.MauiClient.Views.CartView
{
    public partial class CartNotAvailableView : ContentPage
    {
        public CartNotAvailableView(CartNotAvailableViewModel cartNotAvailableViewModel)
        {
            BindingContext = cartNotAvailableViewModel;
            InitializeComponent();
        }
    }
}