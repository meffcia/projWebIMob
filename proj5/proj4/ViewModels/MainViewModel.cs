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
    public partial class MainViewModel
    {
        private readonly IMessageDialogService _messageDialogService;
        private MainViewModel _mainViewModel;

        public MainViewModel(
            IProductService productService,
            IMessageDialogService messageDialogService)
        {
            _messageDialogService = messageDialogService;
        }

        [RelayCommand]
        public async Task ShowBooks()
        {
            await Shell.Current.GoToAsync(nameof(BookMainPage), true, new Dictionary<string, object>
            {
            });
        }

        [RelayCommand]
        public async Task ShowAuthors()
        {
            await Shell.Current.GoToAsync(nameof(AuthorMainPage), true, new Dictionary<string, object>
            {
            });
        }

        [RelayCommand]
        public async Task ShowWriters()
        {
            await Shell.Current.GoToAsync(nameof(WriterMainPage), true, new Dictionary<string, object>
            {
            });
        }
    }
}
