using System.Collections.ObjectModel;
using System.Windows.Input;
using NobleFinalFramework.Commands;
using NobleFinalFramework.DTOs;
using NobleFinalFramework.Queries;
using NobleFinalSensor.Commands;
using NobleFinalSensor.Stores;

namespace NobleFinalSensor.ViewModels
{
    public class SensorListViewModel : ViewModelBase
    {
        private readonly SensorStore _store;
        private readonly ISensorQueryService? _queryService;
        private readonly ISensorCommandService? _commandService;
        private readonly MainViewModel? _mainViewModel;
        private SensorDTO? _selectedSensor;
        private bool _isLoading;
        private string _searchText = string.Empty;
        private ObservableCollection<SensorDTO> _filteredSensors;

        public ObservableCollection<SensorDTO> Sensors => _store.Sensors;

        public ObservableCollection<SensorDTO> FilteredSensors
        {
            get => _filteredSensors;
            set => SetProperty(ref _filteredSensors, value);
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    FilterSensors();
                }
            }
        }

        public SensorDTO? SelectedSensor
        {
            get => _selectedSensor;
            set => SetProperty(ref _selectedSensor, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public ICommand AddSensorCommand { get; }
        public ICommand EditSensorCommand { get; }
        public ICommand DeleteSensorCommand { get; }

        public SensorListViewModel(SensorStore store, ISensorQueryService? queryService = null, ISensorCommandService? commandService = null, MainViewModel? mainViewModel = null)
        {
            _store = store;
            _queryService = queryService;
            _commandService = commandService;
            _mainViewModel = mainViewModel;
            _filteredSensors = new ObservableCollection<SensorDTO>();

            AddSensorCommand = new RelayCommand(_ => AddSensor());
            EditSensorCommand = new RelayCommand(_ => EditSensor(), _ => SelectedSensor != null);
            DeleteSensorCommand = new RelayCommand(async _ => await DeleteSensorAsync(), _ => SelectedSensor != null);
            
            _store.Sensors.CollectionChanged += (s, e) => FilterSensors();
        }

        public async Task LoadSensorsAsync()
        {
            if (_queryService == null) return;

            IsLoading = true;
            try
            {
                var sensors = await _queryService.GetAllSensorsAsync();
                _store.LoadSensors(sensors);
                FilterSensors();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void FilterSensors()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredSensors = new ObservableCollection<SensorDTO>(Sensors);
            }
            else
            {
                var filtered = Sensors.Where(s =>
                    s.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    s.SensorType.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    s.Location.Contains(SearchText, StringComparison.OrdinalIgnoreCase)
                ).ToList();
                FilteredSensors = new ObservableCollection<SensorDTO>(filtered);
            }
        }

        private void AddSensor()
        {
            _mainViewModel?.NavigateToAddSensor();
        }

        private void EditSensor()
        {
            if (SelectedSensor != null)
            {
                _mainViewModel?.NavigateToEditSensor(SelectedSensor.Id);
            }
        }

        private async Task DeleteSensorAsync()
        {
            if (_commandService == null || SelectedSensor == null) return;

            var result = System.Windows.MessageBox.Show(
                $"Are you sure you want to delete sensor '{SelectedSensor.Name}'?",
                "Confirm Delete",
                System.Windows.MessageBoxButton.YesNo,
                System.Windows.MessageBoxImage.Warning);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                await _commandService.DeleteSensorAsync(SelectedSensor.Id);
                await LoadSensorsAsync();
            }
        }
    }
}