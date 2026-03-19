using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetManager.Models;

namespace FleetManager.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository; // TODO: do wytłumaczenia
    private List<Vehicle> _vehicles = new();

    public VehicleService(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }
    public async Task<IReadOnlyList<Vehicle>> GetVehiclesAsync()
    {
        if (_vehicles.Count == 0)
        {
            _vehicles = await _vehicleRepository.LoadVehiclesAsync();
        }
        return _vehicles;
    }

    public async Task RefuelVehicleAsync(Vehicle vehicle, int fuelAmount)
    {
        if (vehicle.Status == VehicleStatus.InRoute)
        {
            Console.WriteLine($"Vehicle {vehicle.RegistrationNumber} is in route");
            //TODO dodać opcję wyświetlania tego w oknie aplikacji
        }
        vehicle.FuelLevel = Math.Min(100, vehicle.FuelLevel + fuelAmount); // jeśli się przepełni pojazd to ustawia fuel level na 100
        //await _vehicleRepository.SaveVehicleAsync(_vehicles); // zapis do json
    }

    public async Task SendVehicleToRouteAsync(Vehicle vehicle)
    {
        if (vehicle.Status == VehicleStatus.Service)
            Console.WriteLine($"Vehicle {vehicle.RegistrationNumber} is in service");
        if (vehicle.FuelLevel < 15)
            Console.WriteLine($"Vehicle {vehicle.RegistrationNumber} has to low fuel level");
        vehicle.Status = VehicleStatus.InRoute;
    }

    public async Task ChangeVehicleStatusAsync(Vehicle vehicle, VehicleStatus status)
    {
        vehicle.Status = status;
    }

    public async Task AddVehicleAsync(Vehicle vehicle)
    {
        if (_vehicles.Any(v => v.RegistrationNumber == vehicle.RegistrationNumber))
            Console.WriteLine($"Vehicle {vehicle.RegistrationNumber} already exists");        
        _vehicles.Add(vehicle);
    }

    public async Task RemoveVehicleAsync(Vehicle vehicle)
    {
        _vehicles.Remove(vehicle);
    }

    public async Task SaveDataAsync()
    {
        await _vehicleRepository.SaveVehicleAsync(_vehicles);
    }
}