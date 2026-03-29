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
    public Vehicle Vehicle { get; }
    
    public ReactiveCommand<Unit, Unit> RefuelCommand { get; }
    public ReactiveCommand<Unit, Unit> SendToRouteCommand { get; }
    public ReactiveCommand<Unit, Unit> RemoveCommand { get; }

    public VehicleItemViewModel(Vehicle vehicle, IVehicleService vehicleService)
    {
        Vehicle = vehicle;
        _vehicleService = vehicleService;

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
            await _vehicleService.RemoveVehicleAsync(Vehicle);
        });
    }
    
    
}