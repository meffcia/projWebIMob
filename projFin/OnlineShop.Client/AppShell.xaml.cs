using OnlineShop.Client.Views.LoginView;
using OnlineShop.Client.Views;

namespace OnlineShop.Client
{
    public partial class AppShell : Shell
    {
        private ShellContent _loginItem;
        private ShellContent _homeItem;
        private ShellContent _registerItem;
        private ShellContent _cartItem;
        private ShellContent _productItem;
        private ShellContent _checkoutItem;
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CartView), typeof(CartView));
            Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
            Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(ProductView), typeof(ProductView));
            Routing.RegisterRoute(nameof(RegisterView), typeof(RegisterView));
            Routing.RegisterRoute(nameof(CheckoutView), typeof(CheckoutView));

            _loginItem = new ShellContent
            {
                Title = "Login",
                ContentTemplate = new DataTemplate(typeof(LoginView))
            };

            _homeItem = new ShellContent
            {
                Title = "Strona główna",
                ContentTemplate = new DataTemplate(typeof(HomeView))
            };

            _registerItem = new ShellContent
            {
                Title = "Zarejestruj",
                ContentTemplate = new DataTemplate(typeof(RegisterView))
            };

            _cartItem = new ShellContent
            {
                Title = "Koszyk",
                ContentTemplate = new DataTemplate(typeof(CartView))
            };

            _productItem = new ShellContent
            {
                Title = "Produkty",
                ContentTemplate = new DataTemplate(typeof(ProductView))
            };

            _checkoutItem = new ShellContent
            {
                Title = "Podsumowanie zamówienia",
                ContentTemplate = new DataTemplate(typeof(CheckoutView))
            };


            Items.Add(_loginItem);
        }

        public void ShowMainTabs()
        {
            if (Items.Contains(_loginItem))
            {
                Items.Remove(_loginItem);
            }

            if (!Items.Contains(_homeItem))
            {
                Items.Add(_homeItem);
            }
            if (!Items.Contains(_productItem))
            {
                Items.Add(_productItem);
            }
            if (!Items.Contains(_cartItem))
            {
                Items.Add(_cartItem);
            }
        }
    }
}
