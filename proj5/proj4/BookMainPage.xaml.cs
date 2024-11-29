using proj4.ViewModels;

namespace proj4;

public partial class BookMainPage : ContentPage
{
	public BookMainPage(ProductsViewModel productsViewModel)
	{
		BindingContext = productsViewModel;
		InitializeComponent();
	}
}

