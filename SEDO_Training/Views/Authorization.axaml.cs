using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;
using Avalonia.Controls;
using Avalonia.Input;
using ReactiveUI;



namespace SEDO_Training;

public partial class Authorization : UserControl
{
    public Authorization()
    {
        InitializeComponent();
        DataContext = new AuthorizationVM();
    }
    public void RegistrationTextBlock_PointerPressed(object sender, PointerPressedEventArgs e)
    {
        var viewModel = this.DataContext as AuthorizationVM; 
        viewModel?.ToRegistration(); 
    }

}