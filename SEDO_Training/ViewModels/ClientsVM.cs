using System;
using System.Collections.Generic;
using DynamicData;
using System.Linq;
using ReactiveUI;
using SEDO_Training.Models;
using Microsoft.EntityFrameworkCore;

namespace SEDO_Training.ViewModels
{
	public class ClientsVM : ViewModelBase
    {
        private List<User> _userList;
        public List<User> UserList
        {
            get => _userList;
            set => this.RaiseAndSetIfChanged(ref _userList, value);
        }
        public void Load()
        {
            UserList = MainWindowViewModel.myConnection.Users.
                  Include(x => x.RoleNavigation).ToList();
        }
        private User? _currentUser;
        public string CurrentUser => _currentUser?.Login;

        public ClientsVM(User? user = null)
        {
            _currentUser = user;
            Load();
        }
        public void ToLast()
        {
            MainWindowViewModel.Instance.PageContent = new Menu(new MenuVM(_currentUser));
        }
        private string _search;
        public string Search
        {
            get => _search;
            set
            {
                _search = value;
                AllFilters();
            }
        }
        void AllFilters()
        {
            UserList = MainWindowViewModel.myConnection.Users.ToList();
            if (!string.IsNullOrWhiteSpace(_search))
            {
                UserList = UserList.Where(x =>
                    x.Login.ToLower().Contains(_search.ToLower())
                ).ToList();
            }
        }

    }
}