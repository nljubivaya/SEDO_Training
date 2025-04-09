using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;

namespace SEDO_Training;

public partial class Clients : UserControl
{
    public Clients(ClientsVM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    private void EditUserButton_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var userId = (int)button.CommandParameter; 
        var viewModel = (ClientsVM)this.DataContext; 
        viewModel.Update(userId);
    }

}