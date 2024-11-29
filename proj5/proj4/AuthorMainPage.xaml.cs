using proj4.ViewModels;

namespace proj4;

public partial class AuthorMainPage : ContentPage
{
	public AuthorMainPage(WritersViewModel authorsViewModel)
	{
		BindingContext = authorsViewModel;
		InitializeComponent();
	}
}

