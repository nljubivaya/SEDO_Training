using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Test6 : UserControl
{
    public Test6(Test6VM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}