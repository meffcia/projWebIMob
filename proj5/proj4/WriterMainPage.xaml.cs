using proj4.ViewModels;

namespace proj4;

public partial class WriterMainPage : ContentPage
{
	public WriterMainPage(WritersViewModel writersViewModel)
	{
		BindingContext = writersViewModel;
		InitializeComponent();
	}
}

