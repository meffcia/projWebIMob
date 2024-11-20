using ClientApp.Views;

namespace ClientApp
{
    public partial class App : Application
    {
        public App(ProductsPage productsPage)
        {
            InitializeComponent();
            MainPage = new NavigationPage(productsPage);
        }
    }
}
