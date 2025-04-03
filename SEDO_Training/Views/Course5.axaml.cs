using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Course5 : UserControl
{
    public Course5()
    {
        InitializeComponent();
        DataContext = new Course1VM();
    }
}