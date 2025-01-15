using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;
using OnlineShop.Shared.Services.AuthService;
using OnlineShop.Shared.Services.ProductService;
using OnlineShop.Shared.Services.CategoryService;
using OnlineShop.Shared.Services.OrderService;
using OnlineShop.Shared.Services.CartService;
using OnlineShop.MauiClient.Services;
using OnlineShop.MauiClient.Views.LoginView;
using OnlineShop.MauiClient.Views.ProductView;
// using OnlineShop.MauiClient.Views.CartView;
// using OnlineShop.MauiClient.Views.OrderView;
using OnlineShop.MauiClient.ViewModels;

namespace OnlineShop.MauiClient;

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
        services.AddHttpClient("ApiClient", client =>
        {
            client.BaseAddress = new Uri("http://localhost:5020/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        ConfigureAppServices(services);
        ConfigureViewModels(services);
        ConfigureViews(services);
    }
    private static void ConfigureAppServices(IServiceCollection services)
    {
        services.AddSingleton<ICategoryService, CategoryService>();
        services.AddHttpClient<IProductService, ProductService>("ApiClient");
        services.AddHttpClient<IAuthService, AuthService>("ApiClient");
        services.AddHttpClient<IOrderService, OrderService>("ApiClient");
        services.AddHttpClient<ICartService, CartService>("ApiClient");
        services.AddSingleton<SecureStorageService>();
        services.AddSingleton<AuthStateProvider>();
        services.AddSingleton<LocalStorageService>();
    }
    private static void ConfigureViewModels(IServiceCollection services)
    {
        services.AddSingleton<IMainViewModel, MainViewModel>();
        // services.AddSingleton<EditProductViewModel>();
        services.AddSingleton<CategoryViewModel>();
        services.AddSingleton<ProductViewModel>();
        // services.AddTransient<OrderViewModel>();
        // services.AddSingleton<CartViewModel>();
        // services.AddSingleton<CartNotAvailableViewModel>();
        services.AddSingleton<LoginViewModel>();
        services.AddSingleton<RegisterViewModel>();
    }
    private static void ConfigureViews(IServiceCollection services)
    {
        // services.AddSingleton<EditProductWindow>();
        // services.AddSingleton<CategoryView>();
        services.AddTransient<ProductView>();
        // services.AddTransient<OrderView>();
        // services.AddTransient<CartView>();
        // services.AddTransient<CartNotAvailableView>();
        services.AddTransient<LoginView>();
        services.AddTransient<RegisterView>();
    }
}
