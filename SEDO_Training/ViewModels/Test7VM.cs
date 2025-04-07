using System;
using System.Collections.Generic;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
	public class Test7VM : ViewModelBase
    {
        private bool _isButtonVisible;
        public bool IsButtonVisible
        {
            get => _isButtonVisible;
            set => this.RaiseAndSetIfChanged(ref _isButtonVisible, value);
        }
        private User? _currentUser;
        public string CurrentUser => _currentUser?.Login;
        private void UpdateButtonVisibility()
        {
            IsButtonVisible = _currentUser?.Role == 2;
        }
        public Test7VM(User? user = null)
        {
            _currentUser = user;
            UpdateButtonVisibility();
        }
    }
}