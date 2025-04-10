using System;
using System.Collections.Generic;
using System.Linq;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Windows.Input;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
	public class Test2VM : ViewModelBase
    {
        public bool _isButtonVisible;
        public bool IsButtonVisible
        {
            get => _isButtonVisible;
            set => this.RaiseAndSetIfChanged(ref _isButtonVisible, value);
        }
        public void UpdateButtonVisibility()
        {
            IsButtonVisible = _currentUser?.Role == 2;
        }
        public string[][] Answers => Questions2List.Select(q => new[] { q.Answer1, q.Answer2, q.Answer3 }).ToArray();

        public async void Delete(int id)
        {
            var result = await MessageBoxManager.GetMessageBoxStandard("Подтвердить удаление?",
                                                                       "Вы уверены, что хотите удалить этот элемент?",
                                                                       ButtonEnum.YesNo).ShowAsync();

            if (result == ButtonResult.Yes)
            {
                Questions2 delete = MainWindowViewModel.myConnection.Questions2s.First(x => x.Id == id);
                MainWindowViewModel.myConnection.Questions2s.Remove(delete);
                MainWindowViewModel.myConnection.SaveChanges();
                MainWindowViewModel.Instance.PageContent = new Test2(new Test2VM(_currentUser));
            }
            UpdateButtonVisibility();
        }
        public void Update(int id)
        {
            MainWindowViewModel.Instance.PageContent = new AddQ2(id, new AddQ2VM(_currentUser));
            UpdateButtonVisibility();
        }
        public void ToPageAddQ2()
        {
            MainWindowViewModel.Instance.PageContent = new AddQ2(new AddQ2VM(_currentUser));
        }
        private List<Questions2> _questions2List;
        public List<Questions2> Questions2List
        {
            get => _questions2List;
            set => this.RaiseAndSetIfChanged(ref _questions2List, value);
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
        public Test2VM(User? user = null)
        {
            Questions2List = MainWindowViewModel.myConnection.Questions2s.
                                                               ToList();
            CheckAnswersCommand = new RelayCommand(_ => CheckAnswers());
            _currentUser = user;
            UpdateButtonVisibility();
        }

        private void CheckAnswers()
        {
            int correctAnswersCount = 0;

            foreach (var question in Questions2List)
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
                    Tests = 2
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
            MainWindowViewModel.Instance.PageContent = new Tests(new TestsVM(_currentUser));
        }

        private User? _currentUser;
        public string CurrentUser => _currentUser?.Login;
        public void ToCourse2()
        {
            MainWindowViewModel.Instance.PageContent = new Course2(new Course1VM(_currentUser));
        }
    }
}