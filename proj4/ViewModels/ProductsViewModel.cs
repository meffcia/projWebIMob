using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using proj4.MessageBox;
using proj4.Models;
using proj4.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj4.ViewModels
{
    public partial class ProductsViewModel : ObservableObject
    {
        private readonly IProductService _productService;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IConnectivity _connectivity;
    

        [ObservableProperty]
        private ObservableCollection<IProduct> _products;


        [ObservableProperty]
        private IProduct _selectedProduct;

   

        public ProductsViewModel(IProductService productService, IMessageDialogService messageDialogService, IConnectivity connectivity)
        {
            _productService = productService;
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
                Products = new ObservableCollection<IProduct>(result.Data);
            }
        }

        [RelayCommand]
        public async Task Delete()
        {
            await DeleteProduct();
            await Shell.Current.GoToAsync("../", true);
        }

        public async Task DeleteProduct()
        {
            await _productService.DeleteProductAsync(_selectedProduct.Id);

            await GetProductsAsync();
        }
        
        public async Task UpdateProductAsync()
        {
            var result = 
            await _productService.UpdateProductAsync(_selectedProduct);
            if (result.Success)
            {
                await GetProductsAsync();
            }
            else
            {
                _messageDialogService.ShowMessage(result.Message);
            }
        }

        [RelayCommand]
        public async Task ShowDetails(IProduct product)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not avaible!");
                return;
            }

            SelectedProduct = product;
      


            await Shell.Current.GoToAsync(nameof(ProductDetailsView), true, new Dictionary<string, object>
            {
                {"Product",product },
                {nameof(ProductsViewModel), this }
            });
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
                {"Product",SelectedProduct },
                {nameof(ProductsViewModel), this }
            });
        }

        [RelayCommand]
        public async Task Edit(IProduct product)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            // Przekazujemy produkt do edycji
            SelectedProduct = product;

            // Przekierowanie do widoku edycji produktu
            await Shell.Current.GoToAsync(nameof(ProductDetailsView), true, new Dictionary<string, object>
            {
                {"Product", SelectedProduct },  // Przekazanie wybranego produktu
                {nameof(ProductsViewModel), this }  // Przekazanie ViewModelu
            });
        }
    }
}


 