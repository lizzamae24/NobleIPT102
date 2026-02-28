using System.Windows;
using NobleFinalSensor.ViewModels;

namespace NobleFinalSensor
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            
            // Load sensors on startup
            Loaded += async (s, e) => await viewModel.SensorListVM.LoadSensorsAsync();
        }
    }
}