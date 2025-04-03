using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia;
using ReactiveUI;

namespace SEDO_Training.ViewModels
{
	public class Course1VM : ViewModelBase
    {
        public void ToMain()
        {
            MainWindowViewModel.Instance.PageContent = new Menu();
        }
        public void ToTest1()
        {
            MainWindowViewModel.Instance.PageContent = new Test1();
        }
        public void ToTest2()
        {
            MainWindowViewModel.Instance.PageContent = new Test2();
        }
    }
}