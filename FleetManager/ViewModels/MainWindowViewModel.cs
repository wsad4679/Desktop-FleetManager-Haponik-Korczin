using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FleetManager.Services;

namespace FleetManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{

    private readonly IVehicleService _vehicleService;
    private readonly IDIalogService _dialogService;
    public ObservableCollection<VehicleItemViewModel> Vehicles { get; } = new();
    public MainWindowViewModel(IVehicleService vehicleService,  IDIalogService dialogService)
    {
        _vehicleService = vehicleService;
        _dialogService = dialogService;
        _ = LoadVehicles();
    }

    private async Task LoadVehicles()
    {
        var vehicles = await _vehicleService.GetVehiclesAsync(); // bierze gotową listę pojazdów z serwisu
        Console.WriteLine($"MVVM: {vehicles.Count}");
        Vehicles.Clear();
        foreach (var vehicle in vehicles)
        {
            var vm = new VehicleItemViewModel(vehicle, _vehicleService, _dialogService);
            vm.OnRemove = item => Vehicles.Remove(item); // usuwa pojazd z observable collection 
            Vehicles.Add(vm);
        }
    }
}