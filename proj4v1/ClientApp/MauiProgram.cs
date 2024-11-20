using ClientApp.Services;
using ClientApp.ViewModels;
using ClientApp.Views;

namespace ClientApp;

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

        // Rejestracja serwisów
        builder.Services.AddHttpClient<IProductService, ApiProductService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:5001/");
        });

        // Rejestracja ViewModel i widoku
        builder.Services.AddTransient<ProductsViewModel>();
        builder.Services.AddTransient<ProductsPage>();

        return builder.Build();
    }
}
