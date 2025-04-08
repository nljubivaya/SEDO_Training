using System.Collections.Generic;
using ReactiveUI;
using SEDO_Training.Models;
using System.Linq;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;


namespace SEDO_Training.ViewModels
{
    public class MenuVM : ViewModelBase
    {

        private User? _currentUser;
        public string CurrentUser => _currentUser?.Login;
        public bool ButtonEnabled => _currentUser?.Role == 2;

        private List<Course> _courseList;
        public List<Course> CourseList
        {
            get => _courseList;
            set => this.RaiseAndSetIfChanged(ref _courseList, value);
        }

        public int _totalPoints;
        public int TotalPoints
        {
            get => _totalPoints;
            set => this.RaiseAndSetIfChanged(ref _totalPoints, value);
        }

        public MenuVM(User? user = null)
        {
            _currentUser = user;
            Load();
            UpdateButtonVisibility();
            CalculateTotalPoints(); // Вычисляем общее количество очков при инициализации
        }

        private string _search;
        public string Search
        {
            get => _search;
            set
            {
                _search = value;
                AllFilters();
            }
        }

        private bool _isButtonVisible;

        public bool IsButtonVisible
        {
            get => _isButtonVisible;
            set => this.RaiseAndSetIfChanged(ref _isButtonVisible, value);
        }

        private void UpdateButtonVisibility()
        {
            IsButtonVisible = _currentUser?.Role == 2;
        }

        void AllFilters()
        {
            CourseList = MainWindowViewModel.myConnection.Courses.ToList();
            if (!string.IsNullOrWhiteSpace(_search))
            {
                CourseList = CourseList.Where(x =>
                    x.Name.ToLower().Contains(_search.ToLower())
                ).ToList();
            }
        }

        public void Load()
        {
            CourseList = MainWindowViewModel.myConnection.Courses.ToList();
        }

        // Метод для вычисления общего количества очков
        private void CalculateTotalPoints()
        {
            if (_currentUser != null)
            {
                TotalPoints = MainWindowViewModel.myConnection.UsersTests
                    .Where(ut => ut.Users == _currentUser.Id)
                    .Sum(ut => ut.Points ?? 0); 
            }
            else
            {
                TotalPoints = 0; 
            }
        }

        public void ToClients()
        {
            MainWindowViewModel.Instance.PageContent = new Clients(new ClientsVM(_currentUser));
        }
        public void ToTest()
        {
            MainWindowViewModel.Instance.PageContent = new Tests(new TestsVM(_currentUser));
        }

        private async void ShowCourseNotAvailableMessage()
        {
            await MessageBoxManager.GetMessageBoxStandard("Информация", "На данный момент курс отсутствует", ButtonEnum.Ok).ShowAsync();
        }

        public void NavigateToCourse(int id)
        {
            switch (id)
            {
                case 1:
                    MainWindowViewModel.Instance.PageContent = new Course1(new Course1VM(_currentUser));
                    break;
                case 2:
                    MainWindowViewModel.Instance.PageContent = new Course2(new Course1VM(_currentUser));
                    break;
                case 3:
                    MainWindowViewModel.Instance.PageContent = new Course3(new Course1VM(_currentUser));
                    break;
                case 4:
                    MainWindowViewModel.Instance.PageContent = new Course4(new Course1VM(_currentUser));
                    break;
                case 5:
                    MainWindowViewModel.Instance.PageContent = new Course5(new Course1VM(_currentUser));
                    break;
                case 6:
                    MainWindowViewModel.Instance.PageContent = new Course6(new Course1VM(_currentUser));
                    break;
                case 7:
                    MainWindowViewModel.Instance.PageContent = new Course7(new Course1VM(_currentUser));
                    break;
                default:
                    ShowCourseNotAvailableMessage();
                    break;
            }
        }
    }
}
