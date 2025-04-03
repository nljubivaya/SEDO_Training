using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia.ViewModels.Commands;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
	public class AuthorizationVM : ViewModelBase
    {
        private bool _isPasswordVisible;
        public bool IsPasswordVisible
        {
            get => _isPasswordVisible;
            set
            {
                if (_isPasswordVisible != value)
                {
                    _isPasswordVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _login;
        private string _password;

        public string Login
        {
            get => _login;
            set => this.RaiseAndSetIfChanged(ref _login, value);
        }
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        private List<User> _userList;
        public List<User> UserList
        {
            get => _userList;
            set => this.RaiseAndSetIfChanged(ref _userList, value);
        }
        private User? _currentUser;
        public ICommand LoginCommand { get; }
        private async void LoginUser()
        {
            using var db = new PodgotovkaContext(); 
            _currentUser = db.Users.Include(u => u.Role).FirstOrDefault(u => u.Login == Login && u.Password == Password);
        }
        public AuthorizationVM()
        {
            UserList = MainWindowViewModel.myConnection.Users
                .Include(x => x.UsersTests)
                .ToList();
            LoginCommand = new RelayCommand(async (param) => await ExecuteLogin());
        }
        private void OpenUserWindow(User user)
        {
            MainWindowViewModel.Instance.PageContent = new Menu { DataContext = new MenuVM(user) };
        }

        public async Task ExecuteLogin()
        {
            using (var db = new PodgotovkaContext())
            {
                _currentUser = db.Users
                    .Include(u => u.RoleNavigation) 
                    .FirstOrDefault(u => u.Login == Login && u.Password == Password);
            }

            if (_currentUser != null)
            {
                MainWindowViewModel.Instance.PageContent = new Menu { DataContext = new MenuVM(_currentUser) };
            }
            else
            {
                await MessageBoxManager.GetMessageBoxStandard("Окно", "Авторизация не успешна", ButtonEnum.Ok).ShowAsync();
            }
        }
        public void To1()
        {
            MainWindowViewModel.Instance.PageContent = new Test1();
        }

    }
}