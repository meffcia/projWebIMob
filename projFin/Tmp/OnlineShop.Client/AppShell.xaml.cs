using OnlineShop.Client.Views.LoginView;
using OnlineShop.Client.Views.ProductView;

namespace OnlineShop.Client
{
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
}
