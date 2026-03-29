using System.Reactive;
using ReactiveUI;

namespace FleetManager.ViewModels;

public class ConfirmationWindowViewModel : ViewModelBase
{
    public string Message { get; }
    
    public ReactiveCommand<Unit, bool> ConfirmCommand { get; }
    public ReactiveCommand<Unit, bool> CancelCommand { get; }
    
    
    public ConfirmationWindowViewModel(string message)
    {
        Message = message;
        ConfirmCommand = ReactiveCommand.Create(() => true);
        CancelCommand = ReactiveCommand.Create(() => false);
    }   
}