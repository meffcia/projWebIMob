using OnlineShop.MauiClient.Views.LoginView;
using OnlineShop.MauiClient.Views.ProductView;

namespace OnlineShop.MauiClient;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
        Routing.RegisterRoute(nameof(RegisterView), typeof(RegisterView));
        Routing.RegisterRoute(nameof(ProductView), typeof(ProductView));
    }
}
