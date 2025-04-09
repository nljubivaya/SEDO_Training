using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class EditUser : UserControl
{
    public EditUser(EditUserVM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    public EditUser(int id, EditUserVM viewModel)
    {
        InitializeComponent();
        DataContext = new EditUserVM(id);
        DataContext = viewModel;
    }
}