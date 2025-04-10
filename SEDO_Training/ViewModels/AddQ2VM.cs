using System;
using System.Collections.Generic;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Linq;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
	public class AddQ2VM : ViewModelBase
    {
        private User? _currentUser;
        public string CurrentUser => _currentUser?.Login;
        private Questions2 _newQ;
        public Questions2 NewQ
        {
            get => _newQ;
            set => this.RaiseAndSetIfChanged(ref _newQ, value);
        }
        public AddQ2VM(User? user = null)
        {
            _newQ = new Questions2();
            _currentUser = user;
        }
        public AddQ2VM(int id)
        {
            _newQ = MainWindowViewModel.myConnection.Questions2s.FirstOrDefault(x => x.Id == id) ?? new Questions2();
        }

        public void ToLast()
        {
            MainWindowViewModel.Instance.PageContent = new Test2(new Test2VM(_currentUser));
        }

        public async void AddQ2()
        {
            var result = await MessageBoxManager.GetMessageBoxStandard("Подтвердить действие",
                                                                       "Сохранить вопрос?",
                                                                       ButtonEnum.YesNo).ShowAsync();
            if (result == ButtonResult.Yes)
            {
                if (NewQ.Correctanswerindex < 0 || NewQ.Correctanswerindex > 2)
                {
                    await MessageBoxManager.GetMessageBoxStandard("Ошибка",
                                                                   "Индекс корректного ответа должен быть от 0 до 2.",
                                                                   ButtonEnum.Ok).ShowAsync();
                    return;
                }
                if (NewQ.Id == 0)
                {
                    MainWindowViewModel.myConnection.Questions2s.Add(NewQ);
                }
                else
                {
                    var existingQuestion = MainWindowViewModel.myConnection.Questions2s.FirstOrDefault(q => q.Id == NewQ.Id);
                    if (existingQuestion != null)
                    {
                        existingQuestion.Questiontext = NewQ.Questiontext;
                        existingQuestion.Correctanswerindex = NewQ.Correctanswerindex;
                        existingQuestion.Answer1 = NewQ.Answer1;
                        existingQuestion.Answer2 = NewQ.Answer2;
                        existingQuestion.Answer3 = NewQ.Answer3;
                    }
                }
                MainWindowViewModel.myConnection.SaveChanges();
                MainWindowViewModel.Instance.PageContent = new Test2(new Test2VM(_currentUser));
            }
        }
    }
}
