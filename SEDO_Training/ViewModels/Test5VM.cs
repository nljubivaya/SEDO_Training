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
	public class Test5VM : ViewModelBase
    {
        private bool _isButtonVisible;
        public bool IsButtonVisible
        {
            get => _isButtonVisible;
            set => this.RaiseAndSetIfChanged(ref _isButtonVisible, value);
        }
        public string[][] Answers => Questions5List.Select(q => new[] { q.Answer1, q.Answer2, q.Answer3 }).ToArray();
        private void UpdateButtonVisibility()
        {
            IsButtonVisible = _currentUser?.Role == 2;
        }
        public async void Delete(int id)
        {
            var result = await MessageBoxManager.GetMessageBoxStandard("����������� ��������?",
                                                                       "�� �������, ��� ������ ������� ���� �������?",
                                                                       ButtonEnum.YesNo).ShowAsync();

            if (result == ButtonResult.Yes)
            {
                Questions5 delete = MainWindowViewModel.myConnection.Questions5s.First(x => x.Id == id);
                MainWindowViewModel.myConnection.Questions5s.Remove(delete);
                MainWindowViewModel.myConnection.SaveChanges();
                MainWindowViewModel.Instance.PageContent = new Test_5(new Test5VM(_currentUser));
            }
        }
        public void Update(int id)
        {
            MainWindowViewModel.Instance.PageContent = new AddQ5(id, new AddQ5VM(_currentUser));
        }
        public void ToPageAddQ5()
        {
            MainWindowViewModel.Instance.PageContent = new AddQ5(new AddQ5VM(_currentUser));
        }
        private List<Questions5> _questions5List;
        public List<Questions5> Questions5List
        {
            get => _questions5List;
            set => this.RaiseAndSetIfChanged(ref _questions5List, value);
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
        public Test5VM(User? user = null)
        {
            Questions5List = MainWindowViewModel.myConnection.Questions5s.
                                                               ToList();
            CheckAnswersCommand = new RelayCommand(_ => CheckAnswers());
            _currentUser = user;
            UpdateButtonVisibility();
        }
        private void CheckAnswers()
        {
            int correctAnswersCount = 0;

            foreach (var question in Questions5List)
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
            MessageBoxManager.GetMessageBoxStandard($"���������", $"�����: {correctAnswersCount} ", ButtonEnum.Ok).ShowAsync();
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
                    Tests = 5
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
        public void ToCourse5()
        {
            MainWindowViewModel.Instance.PageContent = new Course5(new Course1VM(_currentUser));
        }
    }
}