using OnlineShop.MauiClient.ViewModels;

namespace OnlineShop.MauiClient.Views.ProductView
{
    public partial class ProductView : ContentPage
    {
        public ProductView(ProductViewModel productsViewModel)
        {
            BindingContext = productsViewModel;
            InitializeComponent();
        }
    }
}