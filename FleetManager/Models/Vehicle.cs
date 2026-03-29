using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace FleetManager.Models;

public enum VehicleStatus // enum aby wyeliminować możliwe błędy przy zmienie statusu
{
    Available,
    Service,
    InRoute
}


public class Vehicle: ReactiveObject // dziedziczy aby ułatwić informowanie o zmianie obiektu i wprowadzić zmianę w wyglądzie
{
    [Reactive] public string VehicleName { get; set; } = string.Empty; // [Reactive] - skrócenie kodu za pomocą Foody, do informowania wyglądu o zmianie
    [Reactive] public string RegistrationNumber { get; set; } = string.Empty; // string.Empty po to aby ustawić pusty tekst, żeby UI i MVVM nie robiło problemów z nullem
    [Reactive] public int FuelLevel { get; set; }
    [Reactive] public VehicleStatus Status { get; set; } = VehicleStatus.Available; // do enum odwołojemy się jak do obiektu 
}