using Microsoft.Extensions.Configuration;

namespace NobleFinalSensor.Configuration
{
    public static class ConfigurationHelper
    {
        private static IConfiguration? _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string GetConnectionString(string name = "DefaultConnection")
        {
            if (_configuration == null)
            {
                throw new InvalidOperationException("Configuration has not been initialized.");
            }

            return _configuration.GetConnectionString(name) 
                ?? throw new InvalidOperationException($"Connection string '{name}' not found.");
        }
    }
}
