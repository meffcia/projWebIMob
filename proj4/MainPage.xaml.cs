using proj4.ViewModels;

namespace proj4;

public partial class MainPage : ContentPage
{
	public MainPage(ProductsViewModel productsViewModel)
	{
		BindingContext = productsViewModel;
		InitializeComponent();
	}
}

