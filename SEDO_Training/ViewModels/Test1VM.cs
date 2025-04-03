using System;
using System.Collections.Generic;
using ReactiveUI;

namespace SEDO_Training.ViewModels
{
	public class Test1VM : ViewModelBase
    {


        public void ToMain()
        {
            MainWindowViewModel.Instance.PageContent = new Menu();
        }
    }
}