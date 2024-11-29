using proj4.ViewModels;

namespace proj4;

public partial class WriterDetailsView : ContentPage
{
	public WriterDetailsView(WriterDetailsViewModel writerDetailsViewModel)
	{
		BindingContext = writerDetailsViewModel;
		InitializeComponent();
	}
}