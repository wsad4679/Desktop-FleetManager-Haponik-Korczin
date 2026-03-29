using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using FleetManager.Services;
using FleetManager.ViewModels;
using FleetManager.Views;

namespace FleetManager;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var repository = new JsonVehicleRepository();
            var service = new VehicleService(repository); //TODO do wytłumaczenia
            var dialogService = new DialogService();
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(service, dialogService),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}