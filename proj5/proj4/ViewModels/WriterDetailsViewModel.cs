using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using proj4.MessageBox;
using proj4.Services;
using proj5.Domain.Models;
using System;
using System.Threading.Tasks;

namespace proj4.ViewModels
{
    [QueryProperty(nameof(WriterId), "WriterId")]
    [QueryProperty(nameof(Writer), nameof(Writer))]
    [QueryProperty(nameof(WritersViewModel), nameof(WritersViewModel))]
    public partial class WriterDetailsViewModel : ObservableObject
    {
        private readonly IWriterService _writerService;
        private readonly IMessageDialogService _messageDialogService;
        private WritersViewModel _writersViewModel;

        public WriterDetailsViewModel(
            IWriterService writerService,
            IMessageDialogService messageDialogService)
        {
            _writerService = writerService;
            _messageDialogService = messageDialogService;

            ResetForm();
        }

        [ObservableProperty]
        private Writer writer;
        private int _writerId;

        // QueryProperty to bind the WriterId to the parameter in the query
        public int WriterId
        {
            get => _writerId;
            set
            {
                SetProperty(ref _writerId, value);
                Task.Run(async () => await LoadWriterAsync(_writerId));
            }
        }

        // Method to load the writer by ID
        public async Task LoadWriterAsync(int writerId)
        {
            try
            {
                var response = await _writerService.GetWriterByIdAsync(writerId);

                if (response.Success && response.Data != null)
                {
                    Writer = response.Data;
                }
                else
                {
                    _messageDialogService.ShowMessage(response.Message ?? "Writer not found!");
                }
            }
            catch (Exception ex)
            {
                _messageDialogService.ShowMessage($"Error loading writer: {ex.Message}");
            }
        }

        // ViewModel for listing all writers
        public WritersViewModel WritersViewModel
        {
            get { return _writersViewModel; }
            set { _writersViewModel = value; }
        }

        // Command to save the writer (either create or update)
        [RelayCommand]
        public async Task Save()
        {
            if (Writer == null)
            {
                _messageDialogService.ShowMessage("Writer is null. Unable to save.");
                return;
            }

            if (writer.Id == 0)
            {
                await CreateWriterAsync();
            }
            else
            {
                await UpdateWriterAsync();
            }

            ResetForm();

            await Shell.Current.GoToAsync("../", true);
        }

        // Command to cancel the editing and reset the form
        [RelayCommand]
        public void Cancel()
        {
            ResetForm();
            Shell.Current.GoToAsync("../", true);
        }

        // Method to reset the form
        public void ResetForm()
        {
            Writer = new Writer
            {
                Name = "Default",
                Surname = "Author"
            };
        }

        // Method to create a new writer
        public async Task CreateWriterAsync()
        {
            var result = await _writerService.AddWriterAsync(writer);
            if (result.Success)
            {
                await _writersViewModel.GetWritersAsync();
            }
            else
            {
                _messageDialogService.ShowMessage(result.Message);
            }
        }

        // Method to update an existing writer
        public async Task UpdateWriterAsync()
        {
            var result = await _writerService.UpdateWriterAsync(writer);
            if (result.Success)
            {
                await _writersViewModel.GetWritersAsync();
            }
            else
            {
                _messageDialogService.ShowMessage(result.Message);
            }
        }
    }
}
