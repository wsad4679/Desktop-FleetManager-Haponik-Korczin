using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using FleetManager.Models;

namespace FleetManager.Services;

public class JsonVehicleRepository : IVehicleRepository
{
    private const string FilePath = "Assets/vehicles.json";
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public async Task<List<Vehicle>> LoadVehiclesAsync()
    {
        if (!File.Exists(FilePath)) return new List<Vehicle>(); // jeśli plik nie istnieje zwraca pustą listę
        try
        {
            var jsonData = await File.ReadAllTextAsync(FilePath);
            var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(jsonData);
            return vehicles ?? new List<Vehicle>();
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Error while loading vehicles.json file: {exception.Message}");
            return new List<Vehicle>();

        }
    }

    public async Task SaveVehicleAsync(List<Vehicle> vehicles)
    {
        try
        {
            await File.WriteAllTextAsync(FilePath, JsonSerializer.Serialize(vehicles, JsonOptions));
            Console.WriteLine("JSON file saved successfully");
        }catch (Exception exception) when (exception is
                                               IOException or
                                               UnauthorizedAccessException or
                                               JsonException)
            // IOException dysk pełny, plik zablokowany, ścieżka nie istnieje
            // UnauthorizedAccessException brak uprawnień do zapisu w folderze
            /// JsonException coś nie tak z serializacją

        {
            Console.WriteLine($"Save File Error {exception.Message}");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Unexpected error (report this!): {exception.Message}");
        }
    }
}