using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Clients : UserControl
{
    public Clients()
    {
        InitializeComponent();
        DataContext = new ClientsVM();
    }
}