using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class AddQ3 : UserControl
{
    public AddQ3()
    {
        InitializeComponent(); 
        DataContext = new AddQ3VM();
    }
    public AddQ3(int id)
    {
        InitializeComponent();
        DataContext = new AddQ3VM(id);
    }

}