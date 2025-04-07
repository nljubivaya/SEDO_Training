using System;
using System.Collections.Generic;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
	public class Test2VM : ViewModelBase
    {
        public void ToMain()
        {
            MainWindowViewModel.Instance.PageContent = new Menu(new MenuVM(_currentUser));
        }
        private User? _currentUser;
        public string CurrentUser => _currentUser?.Login;
        public void ToCourse2()
        {
            MainWindowViewModel.Instance.PageContent = new Course2(new Course1VM(_currentUser));
        }
    }
}