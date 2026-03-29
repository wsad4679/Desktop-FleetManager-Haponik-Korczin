using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace FleetManager.Extensions;

public static class AvaloniaExtensions
{
    public static Window? GetMainWindow(this Application app)
        => (app.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
}