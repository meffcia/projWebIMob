using OnlineShop.Client.ViewModels;

namespace OnlineShop.Client.Views
{
    public partial class ProductView : ContentPage
    {
        public ProductView(ProductViewModel productViewModel)
        {
            InitializeComponent();
            BindingContext = productViewModel;

        }
    }
}