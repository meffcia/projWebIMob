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

      
    }
}


 