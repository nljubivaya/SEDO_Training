using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class AddQ6 : UserControl
{
    public AddQ6(AddQ6VM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    public AddQ6(int id, AddQ6VM viewModel)
    {
        InitializeComponent();
        DataContext = new AddQ6VM(id);
        DataContext = viewModel;
    }
}