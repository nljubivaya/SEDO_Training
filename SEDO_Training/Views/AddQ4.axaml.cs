using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class AddQ4 : UserControl
{
    public AddQ4(AddQ4VM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    public AddQ4(int id, AddQ4VM viewModel)
    {
        InitializeComponent();
        DataContext = new AddQ4VM(id);
        DataContext = viewModel;
    }
}