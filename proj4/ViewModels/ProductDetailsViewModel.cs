using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using proj4.MessageBox;
using proj4.Models;
using proj4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj4.ViewModels
{
    [QueryProperty(nameof(IProduct),nameof(IProduct))]
    [QueryProperty(nameof(ProductsViewModel),nameof(ProductsViewModel))]
    public partial class ProductDetailsViewModel : ObservableObject
    {
        private readonly IProductService _productService;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IGeolocation _geolocation;
        private readonly IMap _map;
        private ProductsViewModel _productsViewModel;
        public ProductDetailsViewModel(IProductService productService, IMessageDialogService messageDialogService, IGeolocation geolocation, IMap map)
        {
            _productService = productService;
            _messageDialogService = messageDialogService;
            _geolocation = geolocation;
            _map = map; 
        }

        [ObservableProperty]
        private IProduct product;

        public ProductsViewModel ProductsViewModel
        {
            get { return _productsViewModel;}
            set { _productsViewModel = value; }
        }

        // [RelayCommand]
        // public async Task Delete()
        // {
        //     await DeleteProduct();
        //     await Shell.Current.GoToAsync("../", true);
        // }

        // public async Task DeleteProduct()
        // {
        //     // var result = 
        //     await _productService.DeleteProductAsync(product.Id);
        //     // if (result.Success)
        //     // {
        //         await _productsViewModel.GetProductsAsync();
        //     // }
        //     // else
        //     // {
        //     //     _messageDialogService.ShowMessage(result.Message);
        //     // }
            
        // }


        // [RelayCommand]
        // public async Task Save()
        // {
        //     if (product.Id == 0)
        //     {
        //         // tworzymy nowy produkt 
        //         await CreateProductAsync();
                
        //     }
        //     else
        //     {
        //         // aktualizujemy produkt
        //         // await UpdateProductAsync();
        //     }

        //     await Shell.Current.GoToAsync("../", true);
        // }

        // public async Task CreateProductAsync()
        // {
            // var result = 
            // await _productService.AddProductAsync(product);
            // if (result.Success)
            // {
                // await _productsViewModel.GetProductsAsync();
            // }
            // else
            // {
            //     _messageDialogService.ShowMessage(result.Message);
            // }

        // }



        // public async Task UpdateProductAsync()
        // {
        //     // var result = 
        //     await _productService.UpdateProductAsync(product);
        //     // if (result.Success)
        //     // {
        //         await _productsViewModel.GetProductsAsync();
        //     // }
        //     // else
        //     // {
        //     //     _messageDialogService.ShowMessage(result.Message);
        //     // }
        // }
    }
}
