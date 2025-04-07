using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
	public class Course1VM : ViewModelBase
    {
        public Course1VM(User? user = null)
        {
            _currentUser = user;
        }

        private User? _currentUser;
        public string CurrentUser => _currentUser?.Login;
        public void ToMain()
        {
            MainWindowViewModel.Instance.PageContent = new Menu(new MenuVM(_currentUser));
        }

        public void ToTest1()
        {
            MainWindowViewModel.Instance.PageContent = new Test1();
        }
        public void ToTest2()
        {
            MainWindowViewModel.Instance.PageContent = new Test2();
        }
        public void ToTest3()
        {
            MainWindowViewModel.Instance.PageContent = new Test_3();
        }
        public void ToTest4()
        {
            MainWindowViewModel.Instance.PageContent = new Test_4();
        }
        public void ToTest5()
        {
            MainWindowViewModel.Instance.PageContent = new Test_5();
        }
        public void ToTest6()
        {
            MainWindowViewModel.Instance.PageContent = new Test6();
        }
        public void ToTest7()
        {
            MainWindowViewModel.Instance.PageContent = new Test_7();
        }
    }
}