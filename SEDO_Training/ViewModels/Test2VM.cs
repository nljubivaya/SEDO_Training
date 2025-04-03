using System;
using System.Collections.Generic;
using ReactiveUI;

namespace SEDO_Training.ViewModels
{
	public class Test2VM : ViewModelBase
    {
        public void ToMain()
        {
            MainWindowViewModel.Instance.PageContent = new Menu();
        }
        public void ToCourse2()
        {
            MainWindowViewModel.Instance.PageContent = new Course6();
        }
    }
}