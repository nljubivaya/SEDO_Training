using Avalonia.Controls;
using ReactiveUI;
using SEDO_Training.Models;

namespace SEDO_Training.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static MainWindowViewModel Instance;
        public static PodgotovkaContext myConnection = new PodgotovkaContext();

        // Добавляем новое свойство для MenuVM
        public MenuVM MenuVM { get; private set; }

        public MainWindowViewModel()
        {
            Instance = this;

            // Инициализируем MenuVM
            MenuVM = new MenuVM(); // Убедитесь, что MenuVM имеет конструктор без параметров или передайте необходимые параметры
        }

        UserControl _pageContent = new Authorization();

        public UserControl PageContent
        {
            get => _pageContent;
            set => this.RaiseAndSetIfChanged(ref _pageContent, value);
        }
    }
}
