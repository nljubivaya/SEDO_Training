using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SEDO_Training.Models;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Tests : UserControl
{
    public Tests(TestsVM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    private void OnTestButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is Test test)
        {
            var viewModel = (TestsVM)this.DataContext;
            viewModel.NavigateToTest(test.Id);
        }
    }
}