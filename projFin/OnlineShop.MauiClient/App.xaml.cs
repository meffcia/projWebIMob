using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;

namespace OnlineShop.MauiClient;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new MainPage();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());

		MainPage = new NavigationPage(new MainPage());
	}
}