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
        private bool _isButtonVisible;
        public bool IsButtonVisible
        {
            get => _isButtonVisible;
            set => this.RaiseAndSetIfChanged(ref _isButtonVisible, value);
        }
        public string[][] Answers => Questions3List.Select(q => new[] { q.Answer1, q.Answer2, q.Answer3 }).ToArray();
        private void UpdateButtonVisibility()
        {
            IsButtonVisible = _currentUser?.Role == 2;
        }
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
                MainWindowViewModel.Instance.PageContent = new Test1(new Test1VM(_currentUser));
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
        public Test3VM(User? user = null)
        {
            Questions3List = MainWindowViewModel.myConnection.Questions3s.
                                                               ToList();
            CheckAnswersCommand = new RelayCommand(_ => CheckAnswers());
            _currentUser = user;
            UpdateButtonVisibility();
        }
        private void CheckAnswers()
        {
            int correctAnswersCount = 0;

            foreach (var question in Questions3List)
            {
                // Проверяем, выбран ли ответ
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
            AddPointsToUser(correctAnswersCount);
        }

        private void AddPointsToUser(int points)
        {
            if (_currentUser == null) return;

            var userTestRecord = MainWindowViewModel.myConnection.UsersTests
                .FirstOrDefault(ut => ut.Users == _currentUser.Id);

            if (userTestRecord == null)
            {
                userTestRecord = new UsersTest
                {
                    Users = _currentUser.Id,
                    Points = points,
                    Tests = 3
                };
                MainWindowViewModel.myConnection.UsersTests.Add(userTestRecord);
            }
            else
            {
                userTestRecord.Points = points;
            }
            MainWindowViewModel.myConnection.SaveChanges();
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
