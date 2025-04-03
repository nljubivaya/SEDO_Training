using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
	public class Test1VM : ViewModelBase
    {
        private List<Questions1> _questions1List;
        public List<Questions1> Questions1List
        {
            get => _questions1List;
            set => this.RaiseAndSetIfChanged(ref _questions1List, value);
        }
        public ICommand CheckAnswersCommand { get; }
        public Test1VM()
        {
            Questions1List = MainWindowViewModel.myConnection.Questions1s.
                                                               ToList();
            CheckAnswersCommand = new RelayCommand(_ => CheckAnswers());

        }
        private void CheckAnswers()
        {
            int correctAnswersCount = 0; 

            foreach (var question in Questions1List)
            {
                int selectedAnswerIndex = question.Selectedanswerindex ?? 0; 

                if (selectedAnswerIndex == question.Correctanswerindex)
                {
                    correctAnswersCount++; 
                }
            }

            MessageBoxManager.GetMessageBoxStandard($"Результат", $"{correctAnswersCount} балла", ButtonEnum.Ok).ShowAsync();
        }
        public void ToMain()
        {
            MainWindowViewModel.Instance.PageContent = new Menu();
        }
        public void ToCourse1()
        {
            MainWindowViewModel.Instance.PageContent = new Course1();
        }
    }
}