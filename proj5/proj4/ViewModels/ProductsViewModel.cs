﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using proj4.MessageBox;
using proj4.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proj5.Domain.Models;

namespace proj4.ViewModels
{
    public partial class ProductsViewModel : ObservableObject
    {
        private readonly IProductService _productService;
        private readonly IAuthorService _authorService;
        private readonly IReviewService _reviewService;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IConnectivity _connectivity;
    

        [ObservableProperty]
        private ObservableCollection<Book> _products;
        [ObservableProperty]
        private ObservableCollection<Author> _authors;


        [ObservableProperty]
        private Book _selectedProduct;

   

        public ProductsViewModel(IProductService productService, IAuthorService authorService, IReviewService reviewService, IMessageDialogService messageDialogService, IConnectivity connectivity)
        {
            _productService = productService;
            _reviewService = reviewService;
            _authorService = authorService;
            _messageDialogService = messageDialogService;
            _connectivity = connectivity;

            GetProductsAsync();
        }

        public async Task GetProductsAsync()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not avaible!");
                return;
            }


            var result = await _productService.GetAllProductAsync();
            if (result.Success)
            {
                Products = new ObservableCollection<Book>(result.Data);
            }
        }

        [RelayCommand]
        public async Task Delete(Book product)
        {
            if (product == null) return;

            await _productService.DeleteProductAsync(product.Id);
            await GetProductsAsync();
        }

        [RelayCommand]
        public async Task New()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not avaible!");
                return;
            }

            SelectedProduct = new Book();

            await Shell.Current.GoToAsync(nameof(ProductDetailsView), true, new Dictionary<string, object>
            {
                {"Product", SelectedProduct },
                {nameof(ProductsViewModel), this }
            });
        }

        [RelayCommand]
        public async Task EditProduct(Book product)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            var productId = product?.Id ?? 0;

            await Shell.Current.GoToAsync(nameof(ProductDetailsView), true, new Dictionary<string, object>
            {
                {"Product", product ?? new Book()},
                { "ProductId", productId },
                {nameof(ProductsViewModel), this }
                // { "ProductId", productId },
                // { nameof(ProductsViewModel), this }
            });
        }
    }
}


 