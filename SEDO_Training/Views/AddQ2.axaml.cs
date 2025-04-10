using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class AddQ2 : UserControl
{
    public AddQ2(AddQ2VM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    public AddQ2(int id, AddQ2VM viewModel)
    {
        InitializeComponent();
        //DataContext = new AddQ2VM(id);
        DataContext = viewModel;
    }
}