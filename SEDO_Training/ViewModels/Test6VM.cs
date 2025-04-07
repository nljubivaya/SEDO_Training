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
	public class Test6VM : ViewModelBase
    {
		 public string[][] Answers => Questions6List.Select(q => new[] { q.Answer1, q.Answer2, q.Answer3 }).ToArray();

        public async void Delete(int id)
        {
            var result = await MessageBoxManager.GetMessageBoxStandard("Подтвердить удаление?",
                                                                       "Вы уверены, что хотите удалить этот элемент?",
                                                                       ButtonEnum.YesNo).ShowAsync();

            if (result == ButtonResult.Yes)
            {
                Questions6 delete = MainWindowViewModel.myConnection.Questions6s.First(x => x.Id == id);
                MainWindowViewModel.myConnection.Questions6s.Remove(delete);
                MainWindowViewModel.myConnection.SaveChanges();
                MainWindowViewModel.Instance.PageContent = new Test6(new Test6VM(_currentUser));
            }
        }
        public void Update(int id)
        {
            MainWindowViewModel.Instance.PageContent = new AddQ6(id);
        }
        public void ToPageAddQ6()
        {
            MainWindowViewModel.Instance.PageContent = new AddQ6();
        }
        private List<Questions6> _questions6List;
        public List<Questions6> Questions6List
        {
            get => _questions6List;
            set => this.RaiseAndSetIfChanged(ref _questions6List, value);
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
        public Test6VM(User? user = null)
        {
            Questions6List = MainWindowViewModel.myConnection.Questions6s.
                                                               ToList();
            CheckAnswersCommand = new RelayCommand(_ => CheckAnswers());
            _currentUser = user;
        }
        private void CheckAnswers()
        {
            int correctAnswersCount = 0;

            foreach (var question in Questions6List)
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
        public void ToCourse6()
        {
            MainWindowViewModel.Instance.PageContent = new Course6(new Course1VM(_currentUser));
        }
    }
}