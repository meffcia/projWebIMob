using proj4.ViewModels;

namespace proj4;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute(nameof(BookMainPage), typeof(BookMainPage));
		Routing.RegisterRoute(nameof(ProductDetailsView), typeof(ProductDetailsView));

    }
}
