using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using proj4.Models;
using proj4.Services;
using proj4.MessageBox;
using System.Linq;
using System.Threading.Tasks;

namespace proj4.ViewModels
{
    [QueryProperty(nameof(ProductId), "ProductId")]
    [QueryProperty(nameof(ProductsViewModel), nameof(ProductsViewModel))]
    public partial class EditProductViewModel : ObservableObject
    {
        private readonly IProductService _productService;
        private readonly IMessageDialogService _messageDialogService;
        private ProductsViewModel _productsViewModel;

        [ObservableProperty]
        private IProduct _selectedProduct;

        public ProductsViewModel ProductsViewModel
        {
            get { return _productsViewModel; }
            set { _productsViewModel = value; }
        }

        private int _productId;
        public int ProductId
        {
            get => _productId;
            set
            {
                SetProperty(ref _productId, value);

                // Wczytanie produktu po ustawieniu ID
                Task.Run(async () => await LoadProductAsync(_productId));
            }
        }

        public EditProductViewModel(IProductService productService, IMessageDialogService messageDialogService)
        {
            _productService = productService;
            _messageDialogService = messageDialogService;
        }

        public async Task LoadProductAsync(int productId)
        {
            try
            {
                var response = await _productService.GetProductByIdAsync(productId);

                if (response.Success && response.Data != null)
                {
                    SelectedProduct = response.Data;
                }
                else
                {
                    _messageDialogService.ShowMessage(response.Message ?? "Product not found!");
                }
            }
            catch (Exception ex)
            {
                _messageDialogService.ShowMessage($"Error loading product: {ex.Message}");
            }
        }

        [RelayCommand]
        public async Task Save()
        {
            if (_selectedProduct == null)
            {
                _messageDialogService.ShowMessage("Product is null. Unable to save.");
                return;
            }

            if (_selectedProduct.Id == 0)
            {
                // await CreateProductAsync();
            }
            else
            {
                await UpdateProductAsync();
            }

            // Nawigacja wstecz, jeśli to konieczne
            await Shell.Current.GoToAsync("../", true);
        }

        [RelayCommand]
        public async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async Task UpdateProductAsync()
        {
            var result = 
            await _productService.UpdateProductAsync(_selectedProduct);
            if (result.Success)
            {
                await _productsViewModel.GetProductsAsync();
            }
            else
            {
                _messageDialogService.ShowMessage(result.Message);
            }
        }
    }
}
