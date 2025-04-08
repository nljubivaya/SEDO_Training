using System;
using System.Collections.Generic;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Linq;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
	public class AddQ3VM : ViewModelBase
    {
        private Questions3 _newQ;
        public Questions3 NewQ
        {
            get => _newQ;
            set => this.RaiseAndSetIfChanged(ref _newQ, value);
        }
        public AddQ3VM(User? user = null)
        {
            _newQ = new Questions3();
            _currentUser = user;
        }
        public AddQ3VM(int id)
        {
            _newQ = MainWindowViewModel.myConnection.Questions3s.FirstOrDefault(x => x.Id == id) ?? new Questions3();
        }
        private User? _currentUser;
        public string CurrentUser => _currentUser?.Login;
        public void ToLast()
        {
            MainWindowViewModel.Instance.PageContent = new Test_3(new Test3VM(_currentUser));
        }

        public async void AddQ3()
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
                    MainWindowViewModel.myConnection.Questions3s.Add(NewQ);
                }
                else
                {
                    var existingQuestion = MainWindowViewModel.myConnection.Questions3s.FirstOrDefault(q => q.Id == NewQ.Id);
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
                MainWindowViewModel.Instance.PageContent = new Test_3(new Test3VM(_currentUser));
            }
        }
    }
}