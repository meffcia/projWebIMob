using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using proj4.MessageBox;
using proj4.Services;
using proj5.Domain.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace proj4.ViewModels
{
    public partial class WritersViewModel : ObservableObject
    {
        private readonly IWriterService _writerService;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IConnectivity _connectivity;

        // ObservableCollection do przechowywania listy pisarzy
        [ObservableProperty]
        private ObservableCollection<Writer> _writers;

        // Wybrany pisarz
        [ObservableProperty]
        private Writer _selectedWriter;

        public WritersViewModel(IWriterService writerService, IMessageDialogService messageDialogService, IConnectivity connectivity)
        {
            _writerService = writerService;
            _messageDialogService = messageDialogService;
            _connectivity = connectivity;

            // Pobierz pisarzy przy inicjalizacji
            GetWritersAsync();
        }

        // Metoda do pobierania wszystkich pisarzy
        public async Task GetWritersAsync()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            // Pobierz listę pisarzy
            var result = await _writerService.GetAllWriterAsync();
            if (result.Success)
            {
                Writers = new ObservableCollection<Writer>(result.Data);
            }
            else
            {
                _messageDialogService.ShowMessage(result.Message);
            }
        }

        // Komenda do usuwania pisarza
        [RelayCommand]
        public async Task DeleteWriter(Writer writer)
        {
            if (writer == null) return;

            var result = await _writerService.DeleteWriterAsync(writer.Id);
            if (result.Success)
            {
                // Ponownie pobierz listę pisarzy po usunięciu
                await GetWritersAsync();
            }
            else
            {
                _messageDialogService.ShowMessage(result.Message);
            }
        }

        // Komenda do dodawania nowego pisarza
        [RelayCommand]
        public async Task New()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            SelectedWriter = new Writer(); // Ustaw pustego pisarza

            // Przejdź do widoku szczegółów pisarza
            await Shell.Current.GoToAsync(nameof(WriterDetailsView), true, new Dictionary<string, object>
            {
                {"Writer", SelectedWriter },
                {nameof(WritersViewModel), this }
            });
        }

        // Komenda do edytowania istniejącego pisarza
        [RelayCommand]
        public async Task EditWriter(Writer writer)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            var writerId = writer?.Id ?? 0;

            // Przejdź do widoku szczegółów pisarza
            await Shell.Current.GoToAsync(nameof(WriterDetailsView), true, new Dictionary<string, object>
            {
                {"Writer", writer ?? new Writer()},
                { "WriterId", writerId },
                {nameof(WritersViewModel), this }
            });
        }
    }
}
