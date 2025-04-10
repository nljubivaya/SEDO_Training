using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.Models;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class EditUser : UserControl
{
    //public EditUser(EditUserVM viewModel)
    //{
    //    InitializeComponent();
    //    DataContext = viewModel;
    //}
    public EditUser(int id, User? currentUser)
    {
        InitializeComponent();
        DataContext = new EditUserVM(id, currentUser); // Передаем ID и текущего пользователя
    }
}