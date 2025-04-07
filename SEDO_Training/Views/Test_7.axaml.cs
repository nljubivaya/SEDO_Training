using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Test_7 : UserControl
{
    public Test_7(Test7VM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}