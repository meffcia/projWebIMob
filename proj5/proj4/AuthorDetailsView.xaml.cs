using proj4.ViewModels;

namespace proj4;

public partial class AuthorDetailsView : ContentPage
{
	public AuthorDetailsView(AuthorDetailsViewModel authorDetailsViewModel)
	{
		BindingContext = authorDetailsViewModel;
		InitializeComponent();
	}
}