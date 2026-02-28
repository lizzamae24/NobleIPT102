using System.Windows.Input;
using NobleFinalFramework.Commands;
using NobleFinalFramework.Queries;
using NobleFinalSensor.Commands;
using NobleFinalSensor.Stores;

namespace NobleFinalSensor.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ISensorQueryService? _queryService;
        private readonly ISensorCommandService? _commandService;
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public HomeViewModel HomeVM { get; }
        public SensorListViewModel SensorListVM { get; }
        public ICommand ShowHomeCommand { get; }
        public ICommand ShowSensorListCommand { get; }

        public MainViewModel(SensorStore store, ISensorQueryService? queryService = null, ISensorCommandService? commandService = null)
        {
            _queryService = queryService;
            _commandService = commandService;
            
            HomeVM = new HomeViewModel(this);
            SensorListVM = new SensorListViewModel(store, queryService, commandService, this);
            _currentViewModel = HomeVM;
            
            ShowHomeCommand = new RelayCommand(_ => CurrentViewModel = HomeVM);
            ShowSensorListCommand = new RelayCommand(_ => CurrentViewModel = SensorListVM);
        }

        public void NavigateToAddSensor()
        {
            var formViewModel = new SensorFormViewModel(_commandService);
            formViewModel.SensorSaved += async (s, e) =>
            {
                await SensorListVM.LoadSensorsAsync();
                CurrentViewModel = SensorListVM;
            };
            formViewModel.Cancelled += (s, e) => CurrentViewModel = SensorListVM;
            
            CurrentViewModel = formViewModel;
        }

        public void NavigateToEditSensor(int sensorId)
        {
            var formViewModel = new SensorFormViewModel(_commandService);
            
            // Load sensor data
            if (_queryService != null)
            {
                Task.Run(async () =>
                {
                    var sensor = await _queryService.GetSensorByIdAsync(sensorId);
                    formViewModel.LoadSensor(sensor);
                });
            }
            
            formViewModel.SensorSaved += async (s, e) =>
            {
                await SensorListVM.LoadSensorsAsync();
                CurrentViewModel = SensorListVM;
            };
            formViewModel.Cancelled += (s, e) => CurrentViewModel = SensorListVM;
            
            CurrentViewModel = formViewModel;
        }

        public void NavigateToSensorList()
        {
            CurrentViewModel = SensorListVM;
        }
    }
}