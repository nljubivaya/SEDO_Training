using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Interactivity;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using ReactiveUI;
using SEDO_Training.Models;
using System.Threading.Tasks;
using System.Reactive;

namespace SEDO_Training.ViewModels
{
	public class InputVM : ViewModelBase
    {
        public Offer _newOf;
        public Offer NewOf
        {
            get => _newOf;
            set => this.RaiseAndSetIfChanged(ref _newOf, value);
        }
        public User? _currentUser;
        public string CurrentUser => _currentUser?.Login;
        public ReactiveCommand<Unit, Unit> AddOffCommand { get; }
        public InputVM(User? user = null)
        {
            _currentUser = user;
            _newOf = new Offer();
            AddOffCommand = ReactiveCommand.CreateFromTask(AddOff);
        }
        public async Task AddOff()
        {
            if (string.IsNullOrWhiteSpace(NewOf.Offers))
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка",
                                                               "Поле не должно быть пустым.",
                                                               ButtonEnum.Ok).ShowAsync();
                return;
            }

            var result = await MessageBoxManager.GetMessageBoxStandard("Подтвердить действие",
                                                                        "Сохранить?",
                                                                        ButtonEnum.YesNo).ShowAsync();
            if (result == ButtonResult.Yes)
            {
                if (NewOf.Id == 0)
                {
                    MainWindowViewModel.myConnection.Offers.Add(NewOf);
                }
                MainWindowViewModel.myConnection.SaveChanges();
                MainWindowViewModel.Instance.PageContent = new Menu(new MenuVM(_currentUser));
            }
        }

        public void ToMenu()
        {
            MainWindowViewModel.Instance.PageContent = new Menu(new MenuVM(_currentUser));
        }
        public void OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            ToMenu();
        }
    }
}