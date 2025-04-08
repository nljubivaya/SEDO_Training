using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Registration : UserControl
{
    public Registration()
    {
        InitializeComponent();
        DataContext = new RegistrationVM();
    }
    public void AuthTextBlock_PointerPressed(object sender, PointerPressedEventArgs e)
    {
        var viewModel = this.DataContext as RegistrationVM;
        viewModel?.ToAuth();
    }
}