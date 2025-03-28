using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Course1p1 : UserControl
{
    public Course1p1()
    {
        InitializeComponent();
        DataContext = new Course1VM();
    }
}