using System;
using System.Collections.Generic;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
	public class Test5VM : ViewModelBase
    {
        public void ToMain()
        {
            MainWindowViewModel.Instance.PageContent = new Menu(new MenuVM(_currentUser));
        }
        private User? _currentUser;
        public string CurrentUser => _currentUser?.Login;
        public void ToCourse5()
        {
            MainWindowViewModel.Instance.PageContent = new Course5(new Course1VM(_currentUser));
        }
    }
}