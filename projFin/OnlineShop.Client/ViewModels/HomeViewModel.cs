using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnlineShop.Client.Views;

namespace OnlineShop.Client.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        public HomeViewModel()
        {

        }

        [RelayCommand]
        public async Task NavigateToProduct()
        {
            await Shell.Current.GoToAsync(nameof(ProductView));
        }
    }
}
