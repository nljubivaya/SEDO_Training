using System;
using System.Collections.Generic;
using System.Diagnostics;
using ReactiveUI;
using System.Windows.Input;
using SEDO_Training.Models;
using System.Linq;


namespace SEDO_Training.ViewModels
{
    public class MenuVM : ViewModelBase
    {
        private User? _currentUser;
        public string CurrentUser => _currentUser?.Login;

        List<Course> _courseList;

       // public ICommand NavigateCommand { get; private set; }
        public List<Course> CourseList { get => _courseList; set => this.RaiseAndSetIfChanged(ref _courseList, value); }
        public MenuVM(User? user = null)
        {
            _currentUser = user;
            Load();
           // NavigateCommand = new NavigateCommand();
        }
        string _search;
        private NavigateCommand NavigateCommand;

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

        //кнопка перехода
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is int id)
            {
                // Логика перехода на нужную страницу
                switch (id)
                {
                    case 1:
                        MainWindowViewModel.Instance.PageContent = new Course1();
                        break;
                    case 2:
                        // Переход на страницу курса 2
                        MainWindowViewModel.Instance.PageContent = new Course2();
                        break;
                }
            }
        }
    }

    internal class NavigateCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}