using OnlineShop.Client.ViewModels;

namespace OnlineShop.Client.Views;

public partial class CheckoutView : ContentPage
{
	public CheckoutView(CheckoutViewModel checkoutViewModel)
	{
        InitializeComponent();
        BindingContext = checkoutViewModel;
    }
}