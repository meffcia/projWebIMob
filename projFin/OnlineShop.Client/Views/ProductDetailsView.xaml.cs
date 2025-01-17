using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineShop.Client.ViewModels;

namespace OnlineShop.Client.Views
{
    public partial class ProductDetailsView : ContentPage
    {
        public ProductDetailsView(ProductDetailsViewModel productDetailsViewModel)
        {
            InitializeComponent();
            BindingContext = productDetailsViewModel;
        }
    }
}