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
    public partial class EditProductViewModel : ObservableObject
    {
        private readonly IProductService _productService;
        private readonly IMessageDialogService _messageDialogService;
        private readonly string _filePath = "path_to_your_json_file";  // Zaktualizuj ścieżkę do pliku JSON

        [ObservableProperty]
        private IProduct _selectedProduct;

        public EditProductViewModel(IProductService productService, IMessageDialogService messageDialogService)
        {
            _productService = productService;
            _messageDialogService = messageDialogService;
        }

        // Załaduj produkt na podstawie przekazanego Id
        public async Task LoadProductAsync(int productId)
        {
            try
            {
                var json = await File.ReadAllTextAsync(_filePath);
                var products = JsonConvert.DeserializeObject<List<Book>>(json) ?? new List<Book>();

                var productToEdit = products.FirstOrDefault(p => p.Id == productId);

                if (productToEdit != null)
                {
                    SelectedProduct = productToEdit;
                }
                else
                {
                    _messageDialogService.ShowMessage("Product not found!");
                }
            }
            catch (Exception ex)
            {
                _messageDialogService.ShowMessage($"Error loading product: {ex.Message}");
            }
        }

        [RelayCommand]
        public async Task SaveProductAsync()
        {
            var result = await _productService.UpdateProductAsync(SelectedProduct);
            if (result.Success)
            {
                _messageDialogService.ShowMessage("Product updated successfully!");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                _messageDialogService.ShowMessage(result.Message);
            }
        }

        [RelayCommand]
        public async Task CancelAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
