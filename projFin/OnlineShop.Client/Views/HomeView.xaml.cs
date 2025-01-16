using OnlineShop.Client.ViewModels;

namespace OnlineShop.Client.Views;

public partial class HomeView : ContentPage
{
	public HomeView(HomeViewModel homeViewModel)
	{
        InitializeComponent();
        BindingContext = homeViewModel;
    }
}