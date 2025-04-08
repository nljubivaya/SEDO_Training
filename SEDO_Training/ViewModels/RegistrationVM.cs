using System;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Windows.Input;
using ReactiveUI;
using SEDO_Training.Models;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using Microsoft.EntityFrameworkCore;

namespace SEDO_Training.ViewModels
{
    public class RegistrationVM : ViewModelBase
    {
        private string _login;
        private string _password;
        private bool _isAdmin;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public void ToAuth()
        {
            MainWindowViewModel.Instance.PageContent = new Authorization();
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        public ICommand RegisterCommand { get; }

        public RegistrationVM()
        {
            RegisterCommand = new RelayCommand(Register);
        }
        private async void Register(object parameter)
        {
            // Проверка на пустые поля
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Все поля должны быть заполнены", ButtonEnum.Ok).ShowAsync();
                return;
            }

            using (var context = new PodgotovkaContext())
            {
                try
                {
                    // Проверка на существование пользователя с таким логином
                    var existingUser = await context.Users
                        .FirstOrDefaultAsync(u => u.Login == Login);

                    if (existingUser != null)
                    {
                        await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Пользователь с таким логином уже существует", ButtonEnum.Ok).ShowAsync();
                        return;
                    }

                    // Создание нового пользователя
                    User newUser = new User
                    {
                        Login = Login,
                        Password = Password,
                        Role = IsAdmin ? 2 : 1
                    };

                    context.Users.Add(newUser);
                    await context.SaveChangesAsync();

                    await MessageBoxManager.GetMessageBoxStandard("Успех", "Регистрация прошла успешно", ButtonEnum.Ok).ShowAsync();
                    MainWindowViewModel.Instance.PageContent = new Authorization();
                }
                catch (Exception ex)
                {
                    await MessageBoxManager.GetMessageBoxStandard("Ошибка", $"Не удалось зарегистрировать пользователя: {ex.Message}", ButtonEnum.Ok).ShowAsync();
                }
            }
        }
    }
}