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

        private List<Course> _courseList;
        public List<Course> CourseList
        {
            get => _courseList;
            set => this.RaiseAndSetIfChanged(ref _courseList, value);
        }
        public MenuVM(User? user = null)
        {
            _currentUser = user;
            Load();
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
        public void ToClients()
        {
            MainWindowViewModel.Instance.PageContent = new Clients();
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
