using OnlineShop.Client.ViewModels;

namespace OnlineShop.Client.Views.ProductView
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