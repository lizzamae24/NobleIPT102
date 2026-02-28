using Microsoft.EntityFrameworkCore;
using NobleFinalFramework.DbContexts;
using NobleFinalFramework.DTOs;
using NobleFinalFramework.Queries;

namespace NobleFinalFramework.Services
{
    public class SensorQueryService : ISensorQueryService
    {
        private readonly AppDbContextFactory _contextFactory;

        public SensorQueryService(AppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<SensorDTO>> GetAllSensorsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            
            var sensors = await context.Sensors.ToListAsync();
            
            return sensors.Select(s => new SensorDTO
            {
                Id = s.Id,
                Name = s.Name,
                SensorType = s.SensorType,
                Location = s.Location
            });
        }

        public async Task<SensorDTO> GetSensorByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            
            var sensor = await context.Sensors.FindAsync(id);
            
            if (sensor == null)
                throw new InvalidOperationException($"Sensor with ID {id} not found.");

            return new SensorDTO
            {
                Id = sensor.Id,
                Name = sensor.Name,
                SensorType = sensor.SensorType,
                Location = sensor.Location
            };
        }
    }
}
