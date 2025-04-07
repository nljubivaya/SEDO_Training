using System;
using System.Collections.Generic;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Linq;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
	public class AddQ6VM : ViewModelBase
    {
        private Questions6 _newQ;
        public Questions6 NewQ
        {
            get => _newQ;
            set => this.RaiseAndSetIfChanged(ref _newQ, value);
        }
        public AddQ6VM()
        {
            _newQ = new Questions6();
        }
        public AddQ6VM(int id)
        {
            _newQ = MainWindowViewModel.myConnection.Questions6s.FirstOrDefault(x => x.Id == id) ?? new Questions6();
        }

        public void ToLast()
        {
            MainWindowViewModel.Instance.PageContent = new Test6();
        }

        public async void AddQ6()
        {
            var result = await MessageBoxManager.GetMessageBoxStandard("����������� ��������",
                                                                       "��������� ������?",
                                                                       ButtonEnum.YesNo).ShowAsync();
            if (result == ButtonResult.Yes)
            {
                if (NewQ.Correctanswerindex < 0 || NewQ.Correctanswerindex > 2)
                {
                    await MessageBoxManager.GetMessageBoxStandard("������",
                                                                   "������ ����������� ������ ������ ���� �� 0 �� 2.",
                                                                   ButtonEnum.Ok).ShowAsync();
                    return;
                }
                if (NewQ.Id == 0)
                {
                    MainWindowViewModel.myConnection.Questions6s.Add(NewQ);
                }
                else
                {
                    var existingQuestion = MainWindowViewModel.myConnection.Questions6s.FirstOrDefault(q => q.Id == NewQ.Id);
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
                MainWindowViewModel.Instance.PageContent = new Test6();
            }
        }
    }
}