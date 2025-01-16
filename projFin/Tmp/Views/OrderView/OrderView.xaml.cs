using OnlineShop.MauiClient.ViewModels;

namespace OnlineShop.MauiClient.Views.OrderView
{
    public partial class OrderView : ContentPage
    {
        public OrderView(OrderViewModel ordersViewModel)
        {
            BindingContext = ordersViewModel;
            InitializeComponent();
        }
    }
}