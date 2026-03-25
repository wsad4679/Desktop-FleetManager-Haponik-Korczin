using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FleetManager.Services;

namespace FleetManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{

    private readonly IVehicleService _vehicleService;

    public ObservableCollection<VehicleItemViewModel> Vehicles { get; } = new();
    public MainWindowViewModel(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
        _ = LoadVehicles();
    }

    private async Task LoadVehicles()
    {
        var vehicles = await _vehicleService.GetVehiclesAsync();
        Console.WriteLine($"MVVM: {vehicles.Count}");
        Vehicles.Clear();
        foreach (var vehicle in vehicles)
        {
            Vehicles.Add(new VehicleItemViewModel(vehicle));
        }
    }
}