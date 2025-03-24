using Avalonia.Controls;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static MainWindowViewModel Instance;
        public static PodgotovkaContext myConnection = new PodgotovkaContext();
        public MainWindowViewModel()
        {
            Instance = this;

        }
        UserControl _pageContent = new Authorization();

        public UserControl PageContent
        {
            get => _pageContent;
            set => this.RaiseAndSetIfChanged(ref _pageContent, value);
        }

    }
}
