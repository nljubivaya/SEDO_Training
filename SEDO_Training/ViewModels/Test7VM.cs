using System;
using System.Collections.Generic;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
	public class Test7VM : ViewModelBase
    {
        private User? _currentUser;
        public string CurrentUser => _currentUser?.Login;
        public Test7VM(User? user = null)
        {
            _currentUser = user;
        }
    }
}