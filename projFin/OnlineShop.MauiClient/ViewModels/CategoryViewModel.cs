using System.Collections.ObjectModel;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.Services.CategoryService;

namespace OnlineShop.MauiClient.ViewModels
{
    public class CategoryViewModel : IInitializableViewModel
    {
        private readonly ICategoryService _categoryService;
        public ObservableCollection<Category> Categories { get; set; }

        public CategoryViewModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            LoadCategoriesAsync();
        }

        public async void OnViewShown()
        {
            await LoadCategoriesAsync();
        }

        private async Task LoadCategoriesAsync()
        {
            try
            {
                var response = await _categoryService.GetAllCategoriesAsync();
                Categories.Clear();

                if (response.Success)
                {
                    var categoryDtos = response.Data;
                    foreach (var dto in categoryDtos)
                    {
                        Categories.Add(new Category
                        {
                            Id = dto.Id,
                            Name = dto.Name,
                            Description = dto.Description
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Obsługa błędów - np. logowanie
                Console.WriteLine($"Błąd podczas ładowania kategorii: {ex.Message}");
            }
        }
    }
}
