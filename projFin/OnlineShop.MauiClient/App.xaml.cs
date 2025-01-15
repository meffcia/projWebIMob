using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace OnlineShop.MauiClient
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			MainPage = new AppShell();
		}

		// protected override Window CreateWindow(IActivationState? activationState)
		// {
		// 	return new Window(new AppShell());
		// }
	}
}