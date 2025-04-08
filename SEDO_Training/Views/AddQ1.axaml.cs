using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class AddQ1 : UserControl
{
    public AddQ1(AddQ1VM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    public AddQ1(int id, AddQ1VM viewModel)
    {
        InitializeComponent();
        DataContext = new AddQ1VM(id);
        DataContext = viewModel;
    }

}