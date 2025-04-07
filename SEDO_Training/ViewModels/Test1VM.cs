using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using SEDO_Training.Models;

//public string[] Answers => new[] { Answer1, Answer2, Answer3 };
namespace SEDO_Training.ViewModels
{
	public class Test1VM : ViewModelBase
    {
        public string[][] Answers => Questions1List.Select(q => new[] { q.Answer1, q.Answer2, q.Answer3 }).ToArray();

        public async void Delete(int id)
        {
            var result = await MessageBoxManager.GetMessageBoxStandard("Подтвердить удаление?",
                                                                       "Вы уверены, что хотите удалить этот элемент?",
                                                                       ButtonEnum.YesNo).ShowAsync();

            if (result == ButtonResult.Yes)
            {
                Questions1 delete = MainWindowViewModel.myConnection.Questions1s.First(x => x.Id == id);
                MainWindowViewModel.myConnection.Questions1s.Remove(delete);
                MainWindowViewModel.myConnection.SaveChanges();
                MainWindowViewModel.Instance.PageContent = new Test1();
            }
        }

        public void Update(int id)
        {
            MainWindowViewModel.Instance.PageContent = new AddQ1(id);
        }
        public void ToPageAddQ1()
        {
            MainWindowViewModel.Instance.PageContent = new AddQ1();
        }
        private List<Questions1> _questions1List;
        public List<Questions1> Questions1List
        {
            get => _questions1List;
            set => this.RaiseAndSetIfChanged(ref _questions1List, value);
        }
        public ICommand CheckAnswersCommand { get; }
        private string _yourSelectedAnswer;

        public string YourSelectedAnswer
        {
            get { return _yourSelectedAnswer; }
            set
            {
                _yourSelectedAnswer = value;
                OnPropertyChanged(nameof(YourSelectedAnswer));
            }
        }

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
                // Проверяем, выбран ли ответ
                if (question.Selectedanswerindex.HasValue)
                {
                    int selectedAnswerIndex = question.Selectedanswerindex.Value;

                    // Сравниваем с правильным ответом
                    if (selectedAnswerIndex == question.Correctanswerindex)
                    {
                        correctAnswersCount++;
                    }
                }

                // Устанавливаем Selectedanswerindex в null
                question.Selectedanswerindex = null;
            }

            // Сохраняем изменения в базе данных
            MainWindowViewModel.myConnection.SaveChanges();
            MessageBoxManager.GetMessageBoxStandard($"Результат", $"Итого: {correctAnswersCount} б", ButtonEnum.Ok).ShowAsync();
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