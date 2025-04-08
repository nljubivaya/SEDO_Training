using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Input : UserControl
{
    public Input(InputVM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

}