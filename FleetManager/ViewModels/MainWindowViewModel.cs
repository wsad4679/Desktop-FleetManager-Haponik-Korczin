using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using FleetManager.Extensions;
using FleetManager.Models;
using FleetManager.Services;
using FleetManager.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace FleetManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{

    private readonly IVehicleService _vehicleService;
    private readonly IDialogService _dialogService; // interfejsy aby wiedział jakich operacji może dokonywać na przekazanych obiektach
    public ObservableCollection<VehicleItemViewModel> Vehicles { get; } = new();
    public ReactiveCommand<Vehicle, Unit> EditCommand { get; }
    
    [Reactive] public string NewVehicleName { get; set; } = string.Empty;
    [Reactive] public string NewRegistrationNumber { get; set; } = string.Empty;
    public ReactiveCommand<Unit, Unit> AddVehicleCommand { get; }
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    
    public MainWindowViewModel(IVehicleService vehicleService,  IDialogService dialogService)
    {
        _vehicleService = vehicleService;
        _dialogService = dialogService;
        _ = LoadVehicles(); // wczytanie danych 
        EditCommand = ReactiveCommand.CreateFromTask<Vehicle>(OpenEditWindowAsync);
        
        var canAdd = this.WhenAnyValue( // sprawdza czy chociaż jedno pole jest puste, jeśli tak to canAdd = false
            x => x.NewVehicleName,
            x => x.NewRegistrationNumber,
            (name, reg) =>
                !string.IsNullOrWhiteSpace(name) &&
                !string.IsNullOrWhiteSpace(reg)
        );
        AddVehicleCommand = ReactiveCommand.CreateFromTask(AddVehicleAsync, canAdd);// jeśli false przycisku nie da się klilknąć
        SaveCommand = ReactiveCommand.CreateFromTask(SaveAsync);
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
    
    private async Task OpenEditWindowAsync(Vehicle vehicle)
    {
        await new EditVehicleWindow
        {
            DataContext = new EditVehicleViewModel(vehicle)
        }.ShowDialog(
            Avalonia.Application.Current!.GetMainWindow()!
        );

        // zapis do JSON po edycji
        await _vehicleService.SaveDataAsync();
    }
    
    private async Task AddVehicleAsync()
    {
        if (string.IsNullOrWhiteSpace(NewVehicleName) ||
            string.IsNullOrWhiteSpace(NewRegistrationNumber))
            return;

        var vehicle = new Vehicle
        {
            VehicleName = NewVehicleName.Trim(),
            RegistrationNumber = NewRegistrationNumber.Trim(),
            FuelLevel = 100, // domyślnie poziom paliwa na 100 
            Status = VehicleStatus.Available //domyślnie status available
        };

        await _vehicleService.AddVehicleAsync(vehicle);

        var vm = new VehicleItemViewModel(vehicle, _vehicleService, _dialogService);
        vm.OnRemove = item => Vehicles.Remove(item);

        Vehicles.Add(vm);

        //czyszczenie formularza
        NewVehicleName = string.Empty;
        NewRegistrationNumber = string.Empty;
    }
    
    private async Task SaveAsync()
    {
        await _vehicleService.SaveDataAsync();
    }
}