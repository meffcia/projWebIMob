using OnlineShop.Client.ViewModels;

namespace OnlineShop.Client.Views
{
    public partial class CartView : ContentPage
    {
        public CartView(CartViewModel cartViewModel)
        {
            InitializeComponent();
            BindingContext = cartViewModel;
        }
    }
}