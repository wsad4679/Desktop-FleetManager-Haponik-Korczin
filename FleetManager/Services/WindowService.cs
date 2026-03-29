using System.Linq;
using Avalonia.Controls.ApplicationLifetimes;

namespace FleetManager.Services;

public class WindowService : IWindowService
{
    public void CloseWindow(object viewModel)
    {
        if (App.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;
        desktop.Windows.FirstOrDefault(w => w.DataContext == viewModel)?.Close();
    }
}