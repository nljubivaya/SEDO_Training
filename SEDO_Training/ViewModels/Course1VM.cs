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
        private bool _isButtonVisible;
        public bool IsButtonVisible
        {
            get => _isButtonVisible;
            set => this.RaiseAndSetIfChanged(ref _isButtonVisible, value);
        }
        private void UpdateButtonVisibility()
        {
            IsButtonVisible = _currentUser?.Role == 2;
        }
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
            MainWindowViewModel.Instance.PageContent = new Test1(new Test1VM(_currentUser));
            UpdateButtonVisibility();
        }
        public void ToTest2()
        {
            MainWindowViewModel.Instance.PageContent = new Test2(new Test2VM(_currentUser));
            UpdateButtonVisibility();
        }
        public void ToTest3()
        {
            MainWindowViewModel.Instance.PageContent = new Test_3(new Test3VM(_currentUser));
            UpdateButtonVisibility();
        }
        public void ToTest4()
        {
            MainWindowViewModel.Instance.PageContent = new Test_4(new Test4VM(_currentUser));
            UpdateButtonVisibility();
        }
        public void ToTest5()
        {
            MainWindowViewModel.Instance.PageContent = new Test_5(new Test5VM(_currentUser));
            UpdateButtonVisibility();
        }
        public void ToTest6()
        {
            MainWindowViewModel.Instance.PageContent = new Test6(new Test6VM(_currentUser));
            UpdateButtonVisibility();
        }
        public void ToTest7()
        {
            MainWindowViewModel.Instance.PageContent = new Test_7(new Test7VM(_currentUser));
            UpdateButtonVisibility();
        }
    }
}