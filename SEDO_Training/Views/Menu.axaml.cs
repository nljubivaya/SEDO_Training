using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SEDO_Training.ViewModels;
using System;

namespace SEDO_Training;

public partial class Menu : UserControl
{
    public Menu()
    {
        InitializeComponent();

        DataContext = new MenuVM();
    }
}