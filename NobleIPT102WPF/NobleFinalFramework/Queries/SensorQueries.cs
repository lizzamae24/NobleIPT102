using NobleFinalFramework.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace NobleFinalFramework.Queries
{
    public interface ISensorQueryService
    {
        Task<IEnumerable<SensorDTO>> GetAllSensorsAsync();
        Task<SensorDTO> GetSensorByIdAsync(int id);
    }
}