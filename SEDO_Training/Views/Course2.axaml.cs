using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Course2 : UserControl
{
    public Course2(Course1VM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}