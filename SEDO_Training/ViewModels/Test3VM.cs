using System;
using System.Collections.Generic;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Linq;
using System.Windows.Input;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
	public class Test3VM : ViewModelBase
    {
        public string[][] Answers => Questions3List.Select(q => new[] { q.Answer1, q.Answer2, q.Answer3 }).ToArray();

        public async void Delete(int id)
        {
            var result = await MessageBoxManager.GetMessageBoxStandard("Подтвердить удаление?",
                                                                       "Вы уверены, что хотите удалить этот элемент?",
                                                                       ButtonEnum.YesNo).ShowAsync();

            if (result == ButtonResult.Yes)
            {
                Questions3 delete = MainWindowViewModel.myConnection.Questions3s.First(x => x.Id == id);
                MainWindowViewModel.myConnection.Questions3s.Remove(delete);
                MainWindowViewModel.myConnection.SaveChanges();
                MainWindowViewModel.Instance.PageContent = new Test1();
            }
        }
        public void Update(int id)
        {
            MainWindowViewModel.Instance.PageContent = new AddQ3(id);
        }
        public void ToPageAddQ3()
        {
            MainWindowViewModel.Instance.PageContent = new AddQ3();
        }
        private List<Questions3> _questions3List;
        public List<Questions3> Questions3List
        {
            get => _questions3List;
            set => this.RaiseAndSetIfChanged(ref _questions3List, value);
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

        public Test3VM()
        {
            Questions3List = MainWindowViewModel.myConnection.Questions3s.
                                                               ToList();
            CheckAnswersCommand = new RelayCommand(_ => CheckAnswers());

        }
        private void CheckAnswers()
        {
            int correctAnswersCount = 0;

            foreach (var question in Questions3List)
            {
                if (question.Selectedanswerindex.HasValue)
                {
                    int selectedAnswerIndex = question.Selectedanswerindex.Value;
                    if (selectedAnswerIndex == question.Correctanswerindex)
                    {
                        correctAnswersCount++;
                    }
                }
                question.Selectedanswerindex = null;
            }
            MainWindowViewModel.myConnection.SaveChanges();
            MessageBoxManager.GetMessageBoxStandard($"Результат", $"Итого: {correctAnswersCount} ", ButtonEnum.Ok).ShowAsync();
        }
        public void ToMain()
        {
            MainWindowViewModel.Instance.PageContent = new Menu(new MenuVM(_currentUser));
        }
        private User? _currentUser;
        public string CurrentUser => _currentUser?.Login;
        public void ToCourse3()
        {
            MainWindowViewModel.Instance.PageContent = new Course3(new Course1VM(_currentUser));
        }
    }
}
