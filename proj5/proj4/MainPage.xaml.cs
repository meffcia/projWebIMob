using proj4.ViewModels;

namespace proj4;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel mainViewModel)
	{
		BindingContext = mainViewModel;
		InitializeComponent();
	}
}

