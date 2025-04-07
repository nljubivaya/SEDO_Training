using System;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
	public class Test4VM : ViewModelBase
    {
        List<Test4> _test4List;
        public List<Test4> Test4List { get => _test4List; set => this.RaiseAndSetIfChanged(ref _test4List, value); }
        public Test4VM()
        {
            Test4List = MainWindowViewModel.myConnection.Test4s.ToList();
        }
        public void ToMain()
        {
            MainWindowViewModel.Instance.PageContent = new Menu(new MenuVM(_currentUser));
        }

        private User? _currentUser;
        public string CurrentUser => _currentUser?.Login;
        public void ToCourse4()
        {
            MainWindowViewModel.Instance.PageContent = new Course4(new Course1VM(_currentUser));
        }
    }
}