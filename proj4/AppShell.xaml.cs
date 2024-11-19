namespace proj4;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute(nameof(ProductDetailsView), typeof(ProductDetailsView));
	}
}
