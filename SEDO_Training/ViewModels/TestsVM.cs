using System;
using System.Collections.Generic;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using ReactiveUI;
using SEDO_Training.Models;
using DynamicData;
using System.Linq;

namespace SEDO_Training.ViewModels
{
	public class TestsVM : ViewModelBase
    {
        public TestsVM(User? user = null)
        {
            _currentUser = user;
            Load();
        }
        private User? _currentUser;
        public string CurrentUser => _currentUser?.Login;
        public bool ButtonEnabled => _currentUser?.Role == 2;
        private bool _isButtonVisible;
        public int _totalPoints;
        public int TotalPoints
        {
            get => _totalPoints;
            set => this.RaiseAndSetIfChanged(ref _totalPoints, value);
        }
        public void ToClients()
        {
            MainWindowViewModel.Instance.PageContent = new Clients(new ClientsVM(_currentUser));
        }
        public bool IsButtonVisible
        {
            get => _isButtonVisible;
            set => this.RaiseAndSetIfChanged(ref _isButtonVisible, value);
        }

        private void UpdateButtonVisibility()
        {
            IsButtonVisible = _currentUser?.Role == 2;
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
            TestList = MainWindowViewModel.myConnection.Tests.ToList();
            if (!string.IsNullOrWhiteSpace(_search))
            {
                TestList = TestList.Where(x =>
                    x.Name.ToLower().Contains(_search.ToLower())
                ).ToList();
            }
        }

        public void ToMain()
        {
            MainWindowViewModel.Instance.PageContent = new Menu(new MenuVM(_currentUser));
        }
        private async void ShowTestNotAvailableMessage()
        {
            await MessageBoxManager.GetMessageBoxStandard("Информация", "На данный момент тест отсутствует", ButtonEnum.Ok).ShowAsync();
        }
        public void Load()
        {
            TestList = MainWindowViewModel.myConnection.Tests.ToList();
        }
        public void ToMenu()
        {
            MainWindowViewModel.Instance.PageContent = new Menu(new MenuVM(_currentUser));
        }
        public List<Test> _testList;
        public List<Test> TestList
        {
            get => _testList;
            set => this.RaiseAndSetIfChanged(ref _testList, value);
        }
        public void NavigateToTest(int id)
        {
            switch (id)
            {
                case 1:
                    MainWindowViewModel.Instance.PageContent = new Test1(new Test1VM(_currentUser));
                    break;
                case 2:
                    MainWindowViewModel.Instance.PageContent = new Test2(new Test2VM(_currentUser));
                    break;
                case 3:
                    MainWindowViewModel.Instance.PageContent = new Test_3(new Test3VM(_currentUser));
                    break;
                case 4:
                    MainWindowViewModel.Instance.PageContent = new Test_4(new Test4VM(_currentUser));
                    break;
                case 5:
                    MainWindowViewModel.Instance.PageContent = new Test_5(new Test5VM(_currentUser));
                    break;
                case 6:
                    MainWindowViewModel.Instance.PageContent = new Test6(new Test6VM(_currentUser));
                    break;
                case 7:
                    MainWindowViewModel.Instance.PageContent = new Test_7(new Test7VM(_currentUser));
                    break;
                default:
                    ShowTestNotAvailableMessage();
                    break;
            }
        }
    }
}