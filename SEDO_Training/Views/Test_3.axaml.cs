using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Test_3 : UserControl
{
    public Test_3(Test3VM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}