using Microsoft.Extensions.Logging;
using OnlineShop.Shared.Services.AuthService;
using OnlineShop.Shared.Services.ProductService;
using OnlineShop.Shared.Services.CategoryService;
using OnlineShop.Shared.Services.OrderService;
using OnlineShop.Shared.Services.CartService;
using OnlineShop.Client.Services;
using OnlineShop.Client.ViewModels;
using OnlineShop.Client.Views.LoginView;
using OnlineShop.Client.Views;

namespace OnlineShop.Client
{
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
            services.AddSingleton<IConnectivity>(Connectivity.Current);
            services.AddSingleton<IGeolocation>(Geolocation.Default);
            services.AddSingleton<IMap>(Map.Default);
            //services.AddHttpClient("ApiClient", client =>
            //{
            //    client.BaseAddress = new Uri("http://localhost:5020/");
            //    client.DefaultRequestHeaders.Add("Accept", "application/json");
            //});
            ConfigureAppServices(services);
            ConfigureViewModels(services);
            ConfigureViews(services);
        }
        private static void ConfigureAppServices(IServiceCollection services)
        {
            //services.AddSingleton<ICategoryService, CategoryService>();
            services.AddHttpClient<IProductService, ProductService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5020/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5020/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            //services.AddHttpClient<IOrderService, OrderService>("ApiClient");
            services.AddHttpClient<ICartService, CartService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5020/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            services.AddHttpClient<AuthStateProvider>();
            services.AddHttpClient<SecureStorageService>();
        }
        private static void ConfigureViewModels(IServiceCollection services)
        {
            services.AddSingleton<MainPageViewModel>();
            // services.AddSingleton<MainViewModel>();
            // // services.AddSingleton<EditProductViewModel>();
            // services.AddSingleton<CategoryViewModel>();
            // // services.AddTransient<OrderViewModel>();
            services.AddSingleton<CartViewModel>();
            // // services.AddSingleton<CartNotAvailableViewModel>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<RegisterViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<ProductViewModel>();
        }
        private static void ConfigureViews(IServiceCollection services)
        {
            services.AddSingleton<MainPage>();
            // // services.AddSingleton<EditProductWindow>();
            // // services.AddSingleton<CategoryView>();
            // // services.AddTransient<OrderView>();
            services.AddSingleton<CartView>();
            // // services.AddTransient<CartNotAvailableView>();
            services.AddSingleton<LoginView>();
            services.AddSingleton<RegisterView>();
            services.AddSingleton<HomeView>();
            services.AddSingleton<ProductView>();
        }
    }
}
