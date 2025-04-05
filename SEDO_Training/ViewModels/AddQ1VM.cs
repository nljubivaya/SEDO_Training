using System;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
	public class AddQ1VM : ViewModelBase
    {
        Questions1 _newQ;
        public Questions1 newQ { get => _newQ; set => this.RaiseAndSetIfChanged(ref _newQ, value); }
        public AddQ1VM()
        {
            //_newQ = MainWindowViewModel.myConnection.Questions1s.FirstOrDefault(x => x.Id == id);

        }
        public AddQ1VM(int id)
        {
            _newQ = new Questions1();

        }
        public void ToLast()
        {
            MainWindowViewModel.Instance.PageContent = new Test1();
        }
        public void AddQ1()
        {
            if (newQ.Id == 0)
            {
                //MainWindowViewModel.myConnection.Courses.Add(newQ);
            }
            MainWindowViewModel.myConnection.SaveChanges();
            MainWindowViewModel.Instance.PageContent = new AddQ1();
        }

    }
}