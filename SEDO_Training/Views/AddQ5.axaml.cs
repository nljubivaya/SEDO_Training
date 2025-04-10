using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class AddQ5 : UserControl
{
    public AddQ5(AddQ5VM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    public AddQ5(int id, AddQ5VM viewModel)
    {
        InitializeComponent();
        DataContext = new AddQ5VM(id);
        DataContext = viewModel;
    }

}