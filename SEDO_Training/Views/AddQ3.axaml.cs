using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class AddQ3 : UserControl
{
    public AddQ3(AddQ3VM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    public AddQ3(int id, AddQ3VM viewModel)
    {
        InitializeComponent();
        DataContext = new AddQ3VM(id);
        DataContext = viewModel;
    }

}