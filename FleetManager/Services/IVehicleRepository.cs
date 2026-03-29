using System.Collections.Generic;
using System.Threading.Tasks;
using FleetManager.Models;

namespace FleetManager.Services;

public interface IVehicleRepository // interfejs definiuje jak klasa będzie wczytywać dane z json
{
    Task<List<Vehicle>> LoadVehiclesAsync(); // asynchroniczna metoda wczytująca dane z Json do listy z danymi typi Vehicle
    Task SaveVehicleAsync(List<Vehicle> vehicles); // asynchroniczna metoda zapisująca listę z Vehicle do Json
}