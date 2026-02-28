using System.Windows.Input;
using NobleFinalFramework.Commands;
using NobleFinalFramework.DTOs;
using NobleFinalSensor.Commands;

namespace NobleFinalSensor.ViewModels
{
    public class SensorFormViewModel : ViewModelBase
    {
        private readonly ISensorCommandService? _commandService;
        private int _id;
        private string _name = string.Empty;
        private string _sensorType = string.Empty;
        private string _location = string.Empty;
        private bool _isEditMode;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string SensorType
        {
            get => _sensorType;
            set => SetProperty(ref _sensorType, value);
        }

        public string Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set => SetProperty(ref _isEditMode, value);
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public event EventHandler? SensorSaved;
        public event EventHandler? Cancelled;

        public SensorFormViewModel(ISensorCommandService? commandService = null)
        {
            _commandService = commandService;
            SaveCommand = new RelayCommand(async _ => await SaveAsync(), _ => CanSave());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        public void LoadSensor(SensorDTO sensor)
        {
            Id = sensor.Id;
            Name = sensor.Name;
            SensorType = sensor.SensorType;
            Location = sensor.Location;
            IsEditMode = true;
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                   !string.IsNullOrWhiteSpace(SensorType) &&
                   !string.IsNullOrWhiteSpace(Location);
        }

        private async Task SaveAsync()
        {
            if (_commandService == null) return;

            if (IsEditMode)
            {
                await _commandService.UpdateSensorAsync(Id, Name, SensorType, Location);
            }
            else
            {
                await _commandService.AddSensorAsync(Name, SensorType, Location);
            }

            SensorSaved?.Invoke(this, EventArgs.Empty);
        }

        private void Cancel()
        {
            Cancelled?.Invoke(this, EventArgs.Empty);
        }
    }
}
