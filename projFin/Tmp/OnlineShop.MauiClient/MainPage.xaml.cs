using System.ComponentModel;
using OnlineShop.MauiClient.ViewModels;
using OnlineShop.MauiClient.Views.ProductView;
using OnlineShop.MauiClient.Services;
using OnlineShop.Shared.Services.ProductService;
using OnlineShop.Shared.Services.CartService;
using OnlineShop.Shared.Services.CategoryService;

namespace OnlineShop.MauiClient
{
    private readonly IProductService _productService;
    private readonly AuthStateProvider _authStateProvider;
    private readonly ICartService _cartService;
    private readonly ICategoryService _categoryService;
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _viewModel;

        public MainPage(MainViewModel mainViewModel, IProductService productService, AuthStateProvider authStateProvider, ICartService cartService, ICategoryService categoryService)
        {
            InitializeComponent();
            BindingContext = mainViewModel;

            _productService = productService;
            _authStateProvider = authStateProvider;
            _cartService = cartService;
            _categoryService = categoryService;

            var menuItems = new List<string> { "Home", "Settings", "About" };
            MenuListView.ItemsSource = menuItems;
        }

        private void OpenDrawer_Clicked(object sender, EventArgs e)
        {
            _viewModel.ToggleDrawer();
        }

        private async void MenuListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selectedItem = e.SelectedItem.ToString();
                switch (selectedItem)
                {
                    case "Home":
                        await Navigation.PushAsync(new ProductView(_productService, _authStateProvider, _cartService, _categoryService));
                        break;
                    case "Settings":
                        // await Navigation.PushAsync(new SettingsPage());
                        break;
                    case "About":
                        // await Navigation.PushAsync(new AboutPage());
                        break;
                }

                _viewModel.IsDrawerOpen = false;
            }
        }
    }
}
