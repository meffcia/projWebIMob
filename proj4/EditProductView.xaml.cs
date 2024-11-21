using proj4.ViewModels;
namespace proj4;

public partial class EditProductView : ContentPage
{
    public EditProductView(EditProductViewModel editProductViewModel)
    {
        BindingContext = editProductViewModel;
        InitializeComponent();
    }
}