using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class AuthorsViewModel : ObservableObject
    {
        private readonly IAuthorService _authorService;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IConnectivity _connectivity;

        [ObservableProperty]
        private ObservableCollection<Author> _authors;

        [ObservableProperty]
        private Author _selectedAuthor;

        public AuthorsViewModel(IAuthorService authorService, IMessageDialogService messageDialogService, IConnectivity connectivity)
        {
            _authorService = authorService;
            _messageDialogService = messageDialogService;
            _connectivity = connectivity;

            GetAuthorsAsync();
        }

        // Method to retrieve all authors
        public async Task GetAuthorsAsync()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            var result = await _authorService.GetAllAuthorAsync(); // Get authors instead of books
            if (result.Success)
            {
                Authors = new ObservableCollection<Author>(result.Data);
            }
        }

        // Delete author
        [RelayCommand]
        public async Task Delete(Author author)
        {
            if (author == null) return;

            await _authorService.DeleteAuthorAsync(author.Id);
            await GetAuthorsAsync();
        }

        // Add new author
        [RelayCommand]
        public async Task New()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            SelectedAuthor = new Author();

            await Shell.Current.GoToAsync(nameof(AuthorDetailsView), true, new Dictionary<string, object>
            {
                {"Author", SelectedAuthor },
                {nameof(AuthorsViewModel), this }
            });
        }

        // Edit existing author
        [RelayCommand]
        public async Task EditAuthor(Author author)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            var authorId = author?.Id ?? 0;

            await Shell.Current.GoToAsync(nameof(AuthorDetailsView), true, new Dictionary<string, object>
            {
                {"Author", author ?? new Author()},
                { "AuthorId", authorId },
                {nameof(AuthorsViewModel), this }
            });
        }
    }
}
