using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Test1 : UserControl
{
    public Test1(Test1VM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}