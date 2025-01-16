// using Microsoft.Extensions.Logging;

// namespace OnlineShop.Client
// {
//     public static class MauiProgram
//     {
//         public static MauiApp CreateMauiApp()
//         {
//             var builder = MauiApp.CreateBuilder();
//             builder
//                 .UseMauiApp<App>()
//                 .ConfigureFonts(fonts =>
//                 {
//                     fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
//                     fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
//                 });

// #if DEBUG
//     		builder.Logging.AddDebug();
// #endif

//             return builder.Build();
//         }
//     }
// }
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;
using OnlineShop.Shared.Services.AuthService;
using OnlineShop.Shared.Services.ProductService;
using OnlineShop.Shared.Services.CategoryService;
using OnlineShop.Shared.Services.OrderService;
using OnlineShop.Shared.Services.CartService;
using OnlineShop.Client.Services;
using OnlineShop.Client.Views.LoginView;
using OnlineShop.Client.Views.ProductView;
// using OnlineShop.Client.Views.CartView;
// using OnlineShop.Client.Views.OrderView;
using OnlineShop.Client.ViewModels;
using CommunityToolkit.Maui;


namespace OnlineShop.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
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
        services.AddSingleton<MainViewModel>();
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
