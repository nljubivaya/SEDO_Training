using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Test2 : UserControl
{
    public Test2(Test2VM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}