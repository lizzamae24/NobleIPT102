using System.IO;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NobleFinalFramework.Commands;
using NobleFinalFramework.DbContexts;
using NobleFinalFramework.Queries;
using NobleFinalFramework.Services;
using NobleFinalSensor.Configuration;
using NobleFinalSensor.Stores;
using NobleFinalSensor.ViewModels;

namespace NobleFinalSensor
{
    public partial class App : Application
    {
        private ServiceProvider? _serviceProvider;
        public IConfiguration? Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Load configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
            
            // Initialize configuration helper
            ConfigurationHelper.Initialize(Configuration);

            // Setup dependency injection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            // Run database migrations
            RunMigrations();

            // Show main window
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void RunMigrations()
        {
            var factory = _serviceProvider!.GetRequiredService<AppDbContextFactory>();
            using var context = factory.CreateDbContext();
            context.Database.Migrate();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Register configuration
            services.AddSingleton(Configuration!);

            // Register DbContext Factory
            services.AddSingleton<AppDbContextFactory>();

            // Register services
            services.AddTransient<ISensorQueryService, SensorQueryService>();
            services.AddTransient<ISensorCommandService, SensorCommandService>();

            // Register stores
            services.AddSingleton<SensorStore>();

            // Register ViewModels
            services.AddTransient<MainViewModel>();

            // Register MainWindow
            services.AddTransient<MainWindow>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _serviceProvider?.Dispose();
            base.OnExit(e);
        }
    }
}
