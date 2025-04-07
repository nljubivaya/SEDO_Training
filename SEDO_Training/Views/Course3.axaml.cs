using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Course3 : UserControl
{
    public Course3(Course1VM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}