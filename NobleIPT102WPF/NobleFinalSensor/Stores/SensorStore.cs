using System.Collections.ObjectModel;
using NobleFinalFramework.DTOs;


namespace NobleFinalSensor.Stores
{
    public class SensorStore
    {
        public ObservableCollection<SensorDTO> Sensors { get; } = new();


        public void LoadSensors(IEnumerable<SensorDTO> sensors)
        {
            Sensors.Clear();
            foreach (var s in sensors)
            {
                Sensors.Add(s);
            }
        }
    }
}