using Microsoft.Extensions.Logging;
using proj4.Confguration;
using proj4.Services;
using proj4.MessageBox;
using proj4.ViewModels;

namespace proj4;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		ConfigureServices(builder.Services);
		return builder.Build();
	}

	private static void ConfigureServices(IServiceCollection services)
	{
		ConfigureAppServices(services);
		ConfigureViewModels(services);
		ConfigureViews(services);
	}

    private static void ConfigureAppServices(IServiceCollection services)
    {
        services.AddSingleton<IConnectivity>(Connectivity.Current);
        services.AddSingleton<IGeolocation>(Geolocation.Default);
        services.AddSingleton<IMap>(Map.Default);

        // Rejestracja HttpClient z odpowiednią bazową ścieżką
        services.AddHttpClient<IProductService, BookService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:5062/api/Books"); 
            client.DefaultRequestHeaders.Add("Accept", "application/json"); 
        });

		services.AddHttpClient<IWriterService, WriterService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:5062/api/Writer"); 
            client.DefaultRequestHeaders.Add("Accept", "application/json"); 
        });

		services.AddHttpClient<IAuthorService, AuthorService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:5062/api/Author"); 
            client.DefaultRequestHeaders.Add("Accept", "application/json"); 
        });

        services.AddSingleton<IMessageDialogService, MauiMessageDialogService>();
    }


    private static void ConfigureViewModels(IServiceCollection services)
	{
		services.AddSingleton<MainViewModel>();
		services.AddSingleton<WritersViewModel>();
		services.AddSingleton<WriterDetailsViewModel>();
		services.AddSingleton<AuthorsViewModel>();
		services.AddSingleton<AuthorDetailsViewModel>();
		services.AddSingleton<ProductsViewModel>();
		services.AddSingleton<ProductDetailsViewModel>();
    }

	private static void ConfigureViews(IServiceCollection services)
	{
		services.AddSingleton<MainPage>();
		services.AddSingleton<WriterMainPage>();
		services.AddTransient<WriterDetailsView>();
		services.AddSingleton<AuthorMainPage>();
		services.AddTransient<AuthorDetailsView>();
		services.AddSingleton<BookMainPage>();
		services.AddTransient<ProductDetailsView>();
    }
}
