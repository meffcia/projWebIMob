using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClientApp.Services;
using ClientApp.Models; // Upewnij się, że to jest dodane
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ClientApp.ViewModels
{
    public partial class ProductsViewModel : ObservableObject
    {
        private readonly IProductService _productService;
        private bool _isBusy;

        // Observable collection for products
        public ObservableCollection<IProduct> Products { get; } = new();

        // New book object
        public IProduct NewBook { get; set; } = new Book();

        // Private backing field for IsAddBookPageVisible
        private bool _isAddBookPageVisible;

        // Property for controlling the visibility of the add book form
        public bool IsAddBookPageVisible
        {
            get => _isAddBookPageVisible;
            set => SetProperty(ref _isAddBookPageVisible, value);
        }

        public ProductsViewModel(IProductService productService)
        {
            _productService = productService;
            LoadProductsCommand = new AsyncRelayCommand(LoadProductsAsync);
            ShowAddBookCommand = new RelayCommand(ShowAddBookPage);
            AddBookCommand = new RelayCommand(AddBook);
            CancelCommand = new RelayCommand(CancelAddBook);
        }

        public IAsyncRelayCommand LoadProductsCommand { get; }
        public IRelayCommand ShowAddBookCommand { get; }
        public IRelayCommand AddBookCommand { get; }
        public IRelayCommand CancelCommand { get; }

        private async Task LoadProductsAsync()
        {
            if (_isBusy) return;

            try
            {
                _isBusy = true;
                Products.Clear();

                var products = await _productService.GetAllProductAsync();
                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
            finally
            {
                _isBusy = false;
            }
        }

        private void ShowAddBookPage()
        {
            IsAddBookPageVisible = true;  // Show the add book form
        }

        private void AddBook()
        {
            // Add the new book to the service and the products list
            _productService.AddProductAsync(NewBook);
            Products.Add(NewBook);  // Add the new book to the list of products

            // Hide the add book form
            IsAddBookPageVisible = false;
        }

        private void CancelAddBook()
        {
            // Hide the add book form without adding a book
            IsAddBookPageVisible = false;
        }
    }
}
