using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Test_5 : UserControl
{
    public Test_5(Test5VM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}