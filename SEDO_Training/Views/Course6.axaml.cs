using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Course6 : UserControl
{
    public Course6(Course1VM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}