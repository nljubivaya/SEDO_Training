using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Test_4 : UserControl
{
    public Test_4(Test4VM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}