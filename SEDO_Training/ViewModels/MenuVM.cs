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
        public List<Course> CourseList { get => _courseList; set => this.RaiseAndSetIfChanged(ref _courseList, value); }
        public MenuVM(User? user = null)
        {
            _currentUser = user;
            Load();
        }
        string _search;
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
    }
}