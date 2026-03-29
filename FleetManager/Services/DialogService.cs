using System.Threading.Tasks;
using Avalonia;
using FleetManager.Extensions;
using FleetManager.ViewModels;
using FleetManager.Views;

namespace FleetManager.Services;

public class DialogService: IDIalogService
{
    public async Task<bool> ShowConfirmationDialogAsync(string message)
    {
        var viewModel = new ConfirmationWindowViewModel(message);
        var window = new ConfirmationWindow
        {
            DataContext = viewModel
        };
        var mainWindow = Application.Current?.GetMainWindow();
        var result = await window.ShowDialog<bool>(mainWindow!);
        return result;
    }
}