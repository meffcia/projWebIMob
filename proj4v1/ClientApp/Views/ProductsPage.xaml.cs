using ClientApp.ViewModels;

namespace ClientApp.Views
{
    public partial class ProductsPage : ContentPage
    {
        public ProductsPage(ProductsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel; // Wstrzykniêcie ViewModel
        }
    }
}
