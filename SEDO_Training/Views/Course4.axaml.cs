using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Course4 : UserControl
{
    public Course4()
    {
        InitializeComponent();
        DataContext = new Course1VM();
    }
}