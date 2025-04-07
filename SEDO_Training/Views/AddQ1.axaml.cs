using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class AddQ1 : UserControl
{
    public AddQ1()
    {
        InitializeComponent();
        DataContext = new AddQ1VM();
    }
    public AddQ1(int id)
    {
        InitializeComponent();
        DataContext = new AddQ1VM(id);
    }

}