using System.Collections.Generic;
using System.Threading.Tasks;
using FleetManager.Models;

namespace FleetManager.Services;

public interface IVehicleService
{
    Task<IReadOnlyList<Vehicle>> GetVehiclesAsync(); // bierze listę od JsonVehicleRepository i udostępnia reszcie aplikacji
    Task RefuelVehicleAsync(Vehicle vehicle, int fuelAmount); // tankuje pojazd jeśli status na to będzie odpowiadał
    
    Task SendVehicleToRouteAsync(Vehicle vehicle); // wysyła pojazd w trasę jeśli paliwo i status na to pozwala
    
    Task ChangeVehicleStatusAsync(Vehicle vehicle, VehicleStatus status); // zmienia status pojazdu
    
    Task AddVehicleAsync(Vehicle vehicle); // dodaje pojazd
    
    Task RemoveVehicleAsync(Vehicle vehicle); // usuwa pojazd
    Task SaveDataAsync(); // zapisuje dane w liście (tą listę potem można zapisać w json)
}