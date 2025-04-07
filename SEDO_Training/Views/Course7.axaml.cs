using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Course7 : UserControl
{
    public Course7(Course1VM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}