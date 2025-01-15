using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineShop.Shared.Auth;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Services.CartService;
using OnlineShop.Shared.Services.CategoryService;
using OnlineShop.Shared.Services.ProductService;
using OnlineShop.MauiClient.Views.ProductView;
using OnlineShop.MauiClient.Services;
using OnlineShop.MauiClient.Views;
using System.Threading.Tasks;

namespace OnlineShop.MauiClient.ViewModels
{
    public partial class ProductViewModel : ObservableObject, IInitializableViewModel, IAuthStateObserver
    {
        private IProductService _productService;
        private readonly AuthStateProvider _authStateProvider;
        private ICartService _cartService;
        private ICategoryService _categoryService;
        [ObservableProperty]
        private string searchQuery;

        [ObservableProperty]
        private int selectedCategoryId = 0;

        [ObservableProperty]
        private string sortBy;

        [ObservableProperty]
        private bool descending;

        [ObservableProperty]
        private int currentPage = 1;

        [ObservableProperty]
        private int totalPages = 1;

        [ObservableProperty]
        private int itemsPerPage = 10;

        public ObservableCollection<AddToCartParameters> Products { get; set; } = new ObservableCollection<AddToCartParameters>();
        public ObservableCollection<CategoryDto> Categories { get; set; } = new ObservableCollection<CategoryDto>();
        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private bool isAdmin;

        public bool NotAdminVisibility => !isAdmin;
        public bool AdminVisibility => isAdmin;
        public ProductViewModel(IProductService productService, AuthStateProvider authStateProvider, ICartService cartService, ICategoryService categoryService)
        {
            _productService = productService;
            _authStateProvider = authStateProvider;
            _cartService = cartService;
            _categoryService = categoryService;
            LoadProducts();
            GetCategories();
            _authStateProvider.RegisterObserver(this);
            isAdmin = true;//_authStateProvider.IsUserAdminAsync();
        }

        private async void GetCategories()
        {
            var response = await _categoryService.GetAllCategoriesAsync();
            if (response.Success)
            {
                Categories = new ObservableCollection<CategoryDto>(response.Data);
                Categories.Insert(0, new CategoryDto { Id = 0, Name = "Wszystkie" });
            }
        }

        public async void OnViewShown()
        {
            // SelectedCategoryId ??= 0;
            // CurrentPage = 1;
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            int? categoryFilter = SelectedCategoryId != 0 ? SelectedCategoryId : null;
            try
            {
                var response = await _productService.GetAllProductsAsync(SearchQuery, categoryFilter, SortBy, Descending, CurrentPage, ItemsPerPage);

                if (response.Success)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        Products.Clear();
                        foreach (var product in response.Data.Products)
                        {
                            Products.Add(new AddToCartParameters { Product = product, Quantity = 0 });
                        }
                    });
                    TotalPages = (int)Math.Ceiling((double)response.Data.TotalItems / ItemsPerPage);
                }
                else
                {
                    ErrorMessage = response.Message;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "An unexpected error occurred.";
            }
        }

        [RelayCommand]
        private void AddProduct()
        {
            // var addViewModel = new EditProductViewModel(_productService, new ProductDto(), false);
            // var addWindow = new EditProductWindow(addViewModel);
            // addWindow.ShowDialog();

            OnViewShown();
        }

        [RelayCommand]
        private void Edit(AddToCartParameters parameters)
        {
            var productToEdit = parameters.Product;
            // var editViewModel = new EditProductViewModel(_productService, productToEdit, true);

            // var editWindow = new EditProductWindow(editViewModel);
            // editWindow.ShowDialog();

            OnPropertyChanged(nameof(Products));
        }

        [RelayCommand]
        private async void Delete(AddToCartParameters parameters)
        {
            // var result = MessageBox.Show(
            //     $"Are you sure you want to delete the product '{parameters.Product.Name}'?",
            //     "Confirm Delete",
            //     MessageBoxButton.YesNo,
            //     MessageBoxImage.Warning
            // );

            // if (result == MessageBoxResult.Yes)
            // {
            //     try
            //     {
            //         var productId = parameters.Product.Id;
            //         await _productService.DeleteProductAsync(productId);

            //         Application.Current.Dispatcher.Invoke(() => Products.Remove(parameters));
            //     }
            //     catch (Exception ex)
            //     {
            //         MessageBox.Show($"Failed to delete the product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //     }
            // }
        }

        [RelayCommand]
        private async void AddToCart(AddToCartParameters parameters)
        {
            var IsAuthenticated = await _authStateProvider.IsUserAuthenticatedAsync();
            if (IsAuthenticated)
            {
                var userId = await _authStateProvider.GetUserIdFromTokenAsync();

                if (userId == null)
                {
                    // MessageBox.Show($"Unable to identify the user. Please log in again.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                var quantity = parameters.Quantity;
                var product = parameters.Product;

                if (product.Stock >= quantity && quantity > 0)
                {
                    var addCartItemDto = new AddCartItemDto
                    {
                        UserId = userId.Value,
                        ProductId = product.Id,
                        Quantity = quantity
                    };

                    var response = await _cartService.AddToCartAsync(userId.Value, addCartItemDto);
                    if (response.Success)
                    {
                        // MessageBox.Show($"Dodano pomyślnie.", "Ok", MessageBoxButton.OK, MessageBoxImage.None);
                    }
                    else
                    {
                        // MessageBox.Show($"Niepowodzenie.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    // MessageBox.Show($"Invalid quantity or out of stock.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                // MessageBox.Show($"Please log in", "Login required", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void OnAuthenticationStateChanged()
        {
            Task.Run(async () =>
            {
                isAdmin = await _authStateProvider.IsUserAdminAsync();
                OnPropertyChanged(nameof(NotAdminVisibility));
                OnPropertyChanged(nameof(AdminVisibility));
            });
        }

        [RelayCommand]
        private async Task GoToNextPage()
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
                await LoadProducts();
            }
        }

        [RelayCommand]
        private async Task GoToPreviousPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                await LoadProducts();
            }
        }

        [RelayCommand]
        private async Task Search()
        {
            CurrentPage = 1;
            await LoadProducts();
        }


        [RelayCommand]
        private async Task ApplyFilters()
        {
            CurrentPage = 1;
            await LoadProducts();
        }

        [RelayCommand]
        private async Task SortByName()
        {
            descending = !descending;
            SortBy = "name";
            await LoadProducts();
        }

        [RelayCommand]
        private async Task SortByCategory()
        {
            descending = !descending;
            SortBy = "price";
            await LoadProducts();
        }
    }

    public partial class AddToCartParameters
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
