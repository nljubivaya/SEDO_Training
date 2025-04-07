using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SEDO_Training.Models;
using SEDO_Training.ViewModels;
using System;

namespace SEDO_Training;

public partial class Menu : UserControl
{
    public Menu(MenuVM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel; 
    }
    private void OnCourseButtonClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is Course course)
        {
            var viewModel = (MenuVM)this.DataContext;
            viewModel.NavigateToCourse(course.Id);
        }
    }
}