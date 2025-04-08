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
                MainWindowViewModel.Instance.PageContent = new Test1(new Test1VM(_currentUser));
            }
            UpdateButtonVisibility();
        }
        public void Update(int id)
        {
            MainWindowViewModel.Instance.PageContent = new AddQ1(id, new AddQ1VM(_currentUser));
            UpdateButtonVisibility();
        }
        public void ToPageAddQ1()
        {
            MainWindowViewModel.Instance.PageContent = new AddQ1(new AddQ1VM(_currentUser));
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
        public Test1VM(User? user = null)
        {
            Questions1List = MainWindowViewModel.myConnection.Questions1s.
                                                               ToList();
            CheckAnswersCommand = new RelayCommand(_ => CheckAnswers());
            _currentUser = user;
            UpdateButtonVisibility();
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

                    // Проверяем правильность ответа
                    if (selectedAnswerIndex == question.Correctanswerindex)
                    {
                        correctAnswersCount++; // Увеличиваем счетчик правильных ответов

                    }
                }
                // Сбрасываем выбранный ответ для следующей проверки
                question.Selectedanswerindex = null;
            }

            MainWindowViewModel.myConnection.SaveChanges();
            MessageBoxManager.GetMessageBoxStandard($"Результат", $"Итого: {correctAnswersCount} ", ButtonEnum.Ok).ShowAsync();
            AddPointsToUser(correctAnswersCount);
        

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
                    Tests = 1
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
            MainWindowViewModel.Instance.PageContent = new  Tests(new TestsVM(_currentUser));
        }

        private User? _currentUser;
        public string CurrentUser => _currentUser?.Login;
        public void ToCourse1()
        {
            MainWindowViewModel.Instance.PageContent = new Course1(new Course1VM(_currentUser));
        }

    }
}