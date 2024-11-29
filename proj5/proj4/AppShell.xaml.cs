using proj4.ViewModels;

namespace proj4;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute(nameof(WriterMainPage), typeof(WriterMainPage));
		Routing.RegisterRoute(nameof(AuthorMainPage), typeof(AuthorMainPage));
		Routing.RegisterRoute(nameof(BookMainPage), typeof(BookMainPage));
		Routing.RegisterRoute(nameof(WriterDetailsView), typeof(WriterDetailsView));
		Routing.RegisterRoute(nameof(AuthorDetailsView), typeof(AuthorDetailsView));
		Routing.RegisterRoute(nameof(ProductDetailsView), typeof(ProductDetailsView));

    }
}
