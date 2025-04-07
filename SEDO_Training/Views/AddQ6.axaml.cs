using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class AddQ6 : UserControl
{
    public AddQ6()
    {
        InitializeComponent(); 
        DataContext = new AddQ6VM();
    }
    public AddQ6(int id)
    {
        InitializeComponent();
        DataContext = new AddQ6VM(id);
    }
}