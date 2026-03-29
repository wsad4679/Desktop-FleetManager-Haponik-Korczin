using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FleetManager.Services;

namespace FleetManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{

    private readonly IVehicleService _vehicleService;
    private readonly IDialogService _dialogService; // interfejsy aby wiedział jakich operacji może dokonywać na przekazanych obiektach
    public ObservableCollection<VehicleItemViewModel> Vehicles { get; } = new();
    public MainWindowViewModel(IVehicleService vehicleService,  IDialogService dialogService)
    {
        _vehicleService = vehicleService;
        _dialogService = dialogService;
        _ = LoadVehicles(); // wczytanie danych 
    }

    private async Task LoadVehicles()
    {
        var vehicles = await _vehicleService.GetVehiclesAsync(); // bierze gotową listę pojazdów z serwisu
        Console.WriteLine($"MVVM: {vehicles.Count}");
        Vehicles.Clear(); // czyści UI
        foreach (var vehicle in vehicles)
        {
            var vm = new VehicleItemViewModel(vehicle, _vehicleService, _dialogService); // tworzenie ViewModelu każdego pojazdu 
            vm.OnRemove = item => Vehicles.Remove(item); // usuwa pojazd z observable collection 
            Vehicles.Add(vm);
        }
    }
}