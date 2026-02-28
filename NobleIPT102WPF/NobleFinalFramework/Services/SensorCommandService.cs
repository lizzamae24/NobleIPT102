using NobleFinalDomain.Models;
using NobleFinalFramework.Commands;
using NobleFinalFramework.DbContexts;

namespace NobleFinalFramework.Services
{
    public class SensorCommandService : ISensorCommandService
    {
        private readonly AppDbContextFactory _contextFactory;

        public SensorCommandService(AppDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task AddSensorAsync(string name, string type, string location)
        {
            using var context = _contextFactory.CreateDbContext();
            
            var sensor = new Sensor
            {
                Name = name,
                SensorType = type,
                Location = location
            };

            context.Sensors.Add(sensor);
            await context.SaveChangesAsync();
        }

        public async Task UpdateSensorAsync(int id, string name, string type, string location)
        {
            using var context = _contextFactory.CreateDbContext();
            
            var sensor = await context.Sensors.FindAsync(id);
            if (sensor != null)
            {
                sensor.Name = name;
                sensor.SensorType = type;
                sensor.Location = location;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteSensorAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            
            var sensor = await context.Sensors.FindAsync(id);
            if (sensor != null)
            {
                context.Sensors.Remove(sensor);
                await context.SaveChangesAsync();
            }
        }
    }
}
