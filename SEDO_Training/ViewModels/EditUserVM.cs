using System;
using System.Collections.Generic;
using System.Linq;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using ReactiveUI;
using SEDO_Training.Models;
using Microsoft.EntityFrameworkCore;
using System.Reactive.Subjects;

namespace SEDO_Training.ViewModels
{
    public class EditUserVM : ViewModelBase
    {
        private User? _currentUser;
        public string CurrentUser => _currentUser?.Login;

        private User _newU;
        public User NewU
        {
            get => _newU;
            set => this.RaiseAndSetIfChanged(ref _newU, value);
        }

        public EditUserVM(User? user = null)
        {
            _currentUser = user;
        }

        public EditUserVM(int userId)
        {
            _newU = MainWindowViewModel.myConnection.Users
                .Include(x => x.RoleNavigation)
                .Include(x => x.UsersTests)
                .ThenInclude(x => x.TestsNavigation)
                .FirstOrDefault(x => x.Id == userId) ?? new User();

            UsersTest = NewU.UsersTests.ToList();
            if (_newU == null)
            {
                Console.WriteLine($"Пользователь с ID {userId} не найден.");
            }
        }

        public List<Test> Tests => MainWindowViewModel.myConnection.Tests
            .ToList()
            .Except(_newU.UsersTests.Select(x => x.TestsNavigation))
            .ToList();

        private List<UsersTest>? _UsersTest1;
        public List<UsersTest>? UsersTest
        {
            get => _UsersTest1;
            set => this.RaiseAndSetIfChanged(ref _UsersTest1, value);
        }

        private string _selectedRole;
        public string SelectedRole
        {
            get => _selectedRole;
            set => this.RaiseAndSetIfChanged(ref _selectedRole, value);
        }
        public void ToLast(int userId)
        {
            MainWindowViewModel.Instance.PageContent = new Clients(new ClientsVM(_currentUser));

        }
        public async void AddUs()
        {
            var result = await MessageBoxManager.GetMessageBoxStandard("Подтвердить действие",
                                                                       "Сохранить изменения?",
                                                                       ButtonEnum.YesNo).ShowAsync();
            if (result == ButtonResult.Yes)
            {
                var existingUs = MainWindowViewModel.myConnection.Users.FirstOrDefault(q => q.Id == NewU.Id);
                if (existingUs != null)
                {
                    existingUs.Login = NewU.Login;
                    existingUs.Password = NewU.Password;
                    existingUs.Role = NewU.Role; // Устанавливаем роль из выбранной
                }
                MainWindowViewModel.myConnection.SaveChanges();
                MainWindowViewModel.Instance.PageContent = new Clients(new ClientsVM(_currentUser));
            }
        }
    }
}
  