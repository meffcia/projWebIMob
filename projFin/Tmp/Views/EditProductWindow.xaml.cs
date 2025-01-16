using System.Windows;
using OnlineShop.MauiClient.ViewModels;

namespace OnlineShop.MauiClient.Views.ProductView
{
    public partial class EditProductWindow : Window
    {
        public EditProductWindow(EditProductViewModel editProductViewModel)
        {
            BindingContext = editProductViewModel;
            InitializeComponent();
        }
    }
}