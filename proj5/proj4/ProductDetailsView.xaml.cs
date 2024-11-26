using proj4.ViewModels;

namespace proj4;

public partial class ProductDetailsView : ContentPage
{
	public ProductDetailsView(ProductDetailsViewModel productDetailsViewModel)
	{
		BindingContext = productDetailsViewModel;
		InitializeComponent();
	}
}