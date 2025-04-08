using System;
using System.Collections.Generic;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
	public class Test5VM : ViewModelBase
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
        public Test5VM(User? user = null)
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
        public void ToCourse5()
        {
            MainWindowViewModel.Instance.PageContent = new Course5(new Course1VM(_currentUser));
        }
    }
}