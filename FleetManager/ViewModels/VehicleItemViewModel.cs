// (ViewModel dla UserControl)

using System;
using FleetManager.Models;
using ReactiveUI;

namespace FleetManager.ViewModels;

public class VehicleItemViewModel : ViewModelBase
{
    public Vehicle Vehicle { get; }

    public VehicleItemViewModel(Vehicle vehicle)
    {
        Vehicle = vehicle;
        
    }
    
    
}