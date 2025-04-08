using System;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
	public class Test2VM : ViewModelBase
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
        public Test2VM(User? user = null)
        {
            _currentUser = user;
            UpdateButtonVisibility();
        }
        public void ToMain()
        {
            MainWindowViewModel.Instance.PageContent = new Tests(new TestsVM(_currentUser));
        }
        private User? _currentUser;
        public string CurrentUser => _currentUser?.Login;
        public void ToCourse2()
        {
            MainWindowViewModel.Instance.PageContent = new Course2(new Course1VM(_currentUser));
        }
    }
}