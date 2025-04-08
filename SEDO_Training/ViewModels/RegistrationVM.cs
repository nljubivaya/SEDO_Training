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
            // �������� �� ������ ����
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
            {
                await MessageBoxManager.GetMessageBoxStandard("������", "��� ���� ������ ���� ���������", ButtonEnum.Ok).ShowAsync();
                return;
            }

            using (var context = new PodgotovkaContext())
            {
                try
                {
                    // �������� �� ������������� ������������ � ����� �������
                    var existingUser = await context.Users
                        .FirstOrDefaultAsync(u => u.Login == Login);

                    if (existingUser != null)
                    {
                        await MessageBoxManager.GetMessageBoxStandard("������", "������������ � ����� ������� ��� ����������", ButtonEnum.Ok).ShowAsync();
                        return;
                    }

                    // �������� ������ ������������
                    User newUser = new User
                    {
                        Login = Login,
                        Password = Password,
                        Role = IsAdmin ? 2 : 1
                    };

                    context.Users.Add(newUser);
                    await context.SaveChangesAsync();

                    await MessageBoxManager.GetMessageBoxStandard("�����", "����������� ������ �������", ButtonEnum.Ok).ShowAsync();
                    MainWindowViewModel.Instance.PageContent = new Authorization();
                }
                catch (Exception ex)
                {
                    await MessageBoxManager.GetMessageBoxStandard("������", $"�� ������� ���������������� ������������: {ex.Message}", ButtonEnum.Ok).ShowAsync();
                }
            }
        }
    }
}