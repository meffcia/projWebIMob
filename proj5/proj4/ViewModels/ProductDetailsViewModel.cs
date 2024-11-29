using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using proj4.MessageBox;
using proj4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proj5.Domain.Models;

namespace proj4.ViewModels
{
    [QueryProperty(nameof(ProductId), "ProductId")]
    [QueryProperty(nameof(Book), nameof(Book))]
    [QueryProperty(nameof(ProductsViewModel), nameof(ProductsViewModel))]
    public partial class ProductDetailsViewModel : ObservableObject
    {
        private readonly IProductService _productService;
        private readonly IMessageDialogService _messageDialogService;
        private ProductsViewModel _productsViewModel;

        public ProductDetailsViewModel(
            IProductService productService,
            IMessageDialogService messageDialogService)
        {
            _productService = productService;
            _messageDialogService = messageDialogService;

            ResetForm();
        }

        [ObservableProperty]
        private Book product;
        private int _productId;
        public int ProductId
        {
            get => _productId;
            set
            {
                SetProperty(ref _productId, value);

                Task.Run(async () => await LoadProductAsync(_productId));
            }
        }

        public async Task LoadProductAsync(int productId)
        {
            try
            {
                var response = await _productService.GetProductByIdAsync(productId);

                if (response.Success && response.Data != null)
                {
                    Product = response.Data;
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

        public ProductsViewModel ProductsViewModel
        {
            get { return _productsViewModel; }
            set { _productsViewModel = value; }
        }

        [RelayCommand]
        public async Task Save()
        {
            if (Product == null)
            {
                _messageDialogService.ShowMessage("Product is null. Unable to save.");
                return;
            }

            if (product.Id == 0)
            {
                await CreateProductAsync();
            }
            else
            {
                await UpdateProductAsync();
            }

            ResetForm();

            await Shell.Current.GoToAsync("../", true);
        }

        [RelayCommand]
        public void Cancel()
        {
            ResetForm();

            Shell.Current.GoToAsync("../", true);
        }

        public void ResetForm()
        {
            Product = new Book
            {
                Title = "default",
                AuthorId = 0,
                Price = 0
            };
        }

        public async Task CreateProductAsync()
        {
            var result = await _productService.AddProductAsync(product);
            if (result.Success)
            {
                await _productsViewModel.GetProductsAsync();
            }
            else
            {
                _messageDialogService.ShowMessage(result.Message);
            }
        }

        public async Task UpdateProductAsync()
        {
            var result = await _productService.UpdateProductAsync(product);
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
