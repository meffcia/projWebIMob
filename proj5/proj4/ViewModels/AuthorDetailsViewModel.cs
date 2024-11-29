using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using proj4.MessageBox;
using proj4.Services;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proj5.Domain.Models;

namespace proj4.ViewModels
{
    [QueryProperty(nameof(AuthorId), "AuthorId")]
    [QueryProperty(nameof(Author), nameof(Author))]
    [QueryProperty(nameof(AuthorsViewModel), nameof(AuthorsViewModel))]
    public partial class AuthorDetailsViewModel : ObservableObject
    {
        private readonly IAuthorService _authorService;
        private readonly IMessageDialogService _messageDialogService;
        private AuthorsViewModel _authorsViewModel;

        public AuthorDetailsViewModel(
            IAuthorService authorService,
            IMessageDialogService messageDialogService)
        {
            _authorService = authorService;
            _messageDialogService = messageDialogService;

            ResetForm();
        }

        [ObservableProperty]
        private Author author;
        
        private int _authorId;
        public int AuthorId
        {
            get => _authorId;
            set
            {
                SetProperty(ref _authorId, value);
                Task.Run(async () => await LoadAuthorAsync(_authorId));
            }
        }

        public async Task LoadAuthorAsync(int authorId)
        {
            try
            {
                var response = await _authorService.GetAuthorByIdAsync(authorId);

                if (response.Success && response.Data != null)
                {
                    Author = response.Data;
                }
                else
                {
                    _messageDialogService.ShowMessage(response.Message ?? "Author not found!");
                }
            }
            catch (Exception ex)
            {
                _messageDialogService.ShowMessage($"Error loading author: {ex.Message}");
            }
        }

        public AuthorsViewModel AuthorsViewModel
        {
            get { return _authorsViewModel; }
            set { _authorsViewModel = value; }
        }

        [RelayCommand]
        public async Task Save()
        {
            if (Author == null)
            {
                _messageDialogService.ShowMessage("Author is null. Unable to save.");
                return;
            }

            if (author.Id == 0)
            {
                await CreateAuthorAsync();
            }
            else
            {
                await UpdateAuthorAsync();
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
            Author = new Author
            {
                Name = "default",  // Add other default values as needed
            };
        }

        public async Task CreateAuthorAsync()
        {
            var result = await _authorService.AddAuthorAsync(author);
            if (result.Success)
            {
                await _authorsViewModel.GetAuthorsAsync();
            }
            else
            {
                _messageDialogService.ShowMessage(result.Message);
            }
        }

        public async Task UpdateAuthorAsync()
        {
            var result = await _authorService.UpdateAuthorAsync(author);
            if (result.Success)
            {
                await _authorsViewModel.GetAuthorsAsync();
            }
            else
            {
                _messageDialogService.ShowMessage(result.Message);
            }
        }
    }
}
