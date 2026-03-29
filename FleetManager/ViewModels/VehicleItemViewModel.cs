// (ViewModel dla UserControl)

using System;
using System.Reactive;
using FleetManager.Models;
using FleetManager.Services;
using ReactiveUI;

namespace FleetManager.ViewModels;

public class VehicleItemViewModel : ViewModelBase
{
    private readonly IVehicleService _vehicleService;
    private readonly IDialogService _dialogService;
    public Vehicle Vehicle { get; } // pobranie modelu danych 
    
    public ReactiveCommand<Unit, Unit> RefuelCommand { get; }
    public ReactiveCommand<Unit, Unit> SendToRouteCommand { get; }
    public ReactiveCommand<Unit, Unit> RemoveCommand { get; }
    
    public Action<VehicleItemViewModel>? OnRemove; // to jest aby usunąć pojazd z observable collection z MainWindowViewModel

    public VehicleItemViewModel(Vehicle vehicle, IVehicleService vehicleService, IDialogService dialogService)
    {
        Vehicle = vehicle;
        _vehicleService = vehicleService;
        _dialogService = dialogService;

        var canRefuel = this.WhenAnyValue(
            x => x.Vehicle.Status,
            status => status == VehicleStatus.Available || status == VehicleStatus.Service
        );
        
        var canSend = this.WhenAnyValue(
            x => x.Vehicle.Status,
            x => x.Vehicle.FuelLevel,
            (status, fuel) => status == VehicleStatus.Available && fuel > 15
        );
        
        
        RefuelCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await _vehicleService.RefuelVehicleAsync(Vehicle, 100);
        }, canRefuel);

        SendToRouteCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await _vehicleService.SendVehicleToRouteAsync(Vehicle);
        }, canSend);

        RemoveCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var confirm = await _dialogService
                .ShowConfirmationDialogAsync($"Do you want to remove vehicle {Vehicle.VehicleName}");
            if (!confirm) return;
            await _vehicleService.RemoveVehicleAsync(Vehicle);
            
            OnRemove?.Invoke(this); // informuje MainWindowViewModel aby usunął pojazd
        });
    }
    
    
}