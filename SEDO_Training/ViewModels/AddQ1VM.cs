using System;
using System.Collections.Generic;
using System.Linq;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
	public class AddQ1VM : ViewModelBase
    {
        private Questions1 _newQ;
        public Questions1 NewQ
        {
            get => _newQ;
            set => this.RaiseAndSetIfChanged(ref _newQ, value);
        }
        public AddQ1VM()
        {
            _newQ = new Questions1();
        }
        public AddQ1VM(int id)
        {
            _newQ = MainWindowViewModel.myConnection.Questions1s.FirstOrDefault(x => x.Id == id) ?? new Questions1();
        }

        public void ToLast()
        {
            MainWindowViewModel.Instance.PageContent = new Test1();
        }

        public async void AddQ1()
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
                    return; // Прерываем выполнение метода, если индекс некорректен
                }
                if (NewQ.Id == 0)
                {
                    MainWindowViewModel.myConnection.Questions1s.Add(NewQ);
                }
                else
                {
                    var existingQuestion = MainWindowViewModel.myConnection.Questions1s.FirstOrDefault(q => q.Id == NewQ.Id);
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
                MainWindowViewModel.Instance.PageContent = new Test1();
            }
        }
    }
}