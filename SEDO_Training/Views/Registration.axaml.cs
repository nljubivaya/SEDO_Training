using Avalonia;
using Avalonia.Controls;
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
}