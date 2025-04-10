using System;
using System.Collections.Generic;
using DynamicData;
using System.Linq;
using ReactiveUI;
using SEDO_Training.Models;
using Microsoft.EntityFrameworkCore;
using System.Reactive.Subjects;

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
        private List<Role> _roleFilter;

        public List<Role> RoleFilter
        {
            get
            {
                if (_roleFilter == null)
                {
                    _roleFilter = new List<Role>
                    {
                        new Role() { Id = 0, Role1 = "все" }
                    };

                    var rolesFromDb = MainWindowViewModel.myConnection.Roles?.ToList();
                    if (rolesFromDb != null)
                    {
                        _roleFilter.AddRange(rolesFromDb);
                    }
                }
                return _roleFilter;
            }
        }

        private Role _selectedRoleFilter = null;

        public Role SelectedRoleFilter
        {
            get
            {
                return _selectedRoleFilter ?? RoleFilter[0];
            }
            set
            {
                _selectedRoleFilter = value;
                AllFilters();
            }
        }

        public void Update(int userId)
        {
            MainWindowViewModel.Instance.PageContent = new EditUser(userId, _currentUser); // Передаем текущего пользователя
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
            if (_selectedRoleFilter != null && _selectedRoleFilter.Id != 0)
            {
                UserList = UserList.Where(y => y.RoleNavigation.Id == _selectedRoleFilter.Id).ToList();
            }
        }

    }
}