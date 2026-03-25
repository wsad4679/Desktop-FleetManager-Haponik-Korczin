using Avalonia.Controls;
using FleetManager.Services;
using FleetManager.ViewModels;

namespace FleetManager.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        var repository = new JsonVehicleRepository();
        var service = new VehicleService(repository);
        
        DataContext = new MainWindowViewModel(service);
    }
}