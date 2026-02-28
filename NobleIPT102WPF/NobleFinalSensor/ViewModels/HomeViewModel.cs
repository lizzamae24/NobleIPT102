using System.Windows.Input;
using NobleFinalSensor.Commands;

namespace NobleFinalSensor.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly MainViewModel _mainViewModel;

        public ICommand ViewAllSensorsCommand { get; }

        public HomeViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ViewAllSensorsCommand = new RelayCommand(_ => _mainViewModel.NavigateToSensorList());
        }
    }
}
