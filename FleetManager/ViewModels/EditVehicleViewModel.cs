using System;
using System.Collections.Generic;
using FleetManager.Models;

namespace FleetManager.ViewModels;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;

public class EditVehicleViewModel : ViewModelBase
{
    private readonly Vehicle _vehicle;

    [Reactive] public string VehicleName { get; set; } = string.Empty;
    [Reactive] public string RegistrationNumber { get; set; } = string.Empty;
    [Reactive] public int FuelLevel { get; set; }
    [Reactive] public VehicleStatus Status { get; set; }

    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    public Array Statuses => Enum.GetValues(typeof(VehicleStatus)); // pobranie wszystkich możliwych stanów w jakich może znajdować się pojazd z modelu Vehicle
    public EditVehicleViewModel(Vehicle vehicle)
    {
        _vehicle = vehicle;

        // kopiujemy dane do formularza
        VehicleName = _vehicle.VehicleName;
        RegistrationNumber = _vehicle.RegistrationNumber;
        Status = _vehicle.Status;
        FuelLevel = _vehicle.FuelLevel; //TODO odkomentować jeśli pojazd się nie roztankuje po zmianie

        var isValid = this.WhenAnyValue(
            x => x.VehicleName,
            x => x.RegistrationNumber,
            (name, reg) =>
                !string.IsNullOrWhiteSpace(name) &&
                !string.IsNullOrWhiteSpace(reg)
        );

        SaveCommand = ReactiveCommand.Create(() =>
        {
            //zapisujemy zmiany do modelu
            _vehicle.VehicleName = VehicleName.Trim();
            _vehicle.RegistrationNumber = RegistrationNumber.Trim();
            _vehicle.FuelLevel = FuelLevel;
            _vehicle.Status = Status;

            CloseWindow();
        }, isValid);

        CancelCommand = ReactiveCommand.Create(CloseWindow);
    }

    private void CloseWindow()
    {
        App.WindowService.CloseWindow(this);
    }
}