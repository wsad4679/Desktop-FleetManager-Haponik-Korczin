using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using FleetManager.Services;
using FleetManager.ViewModels;
using FleetManager.Views;

namespace FleetManager;

public partial class App : Application
{
    public static IWindowService WindowService { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        WindowService = new WindowService();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var repository = new JsonVehicleRepository(); // tworzy obiekt który posiada metody wczytania danych
            var service = new VehicleService(repository); //przekazuje obiekt oraz wykonuje metody które posiada aby wczytać dane
            var dialogService = new DialogService(); // stworzenie serwisu który potrafi wyświetlać okienko
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(service, dialogService), // UI dostaje ViewModel z danymi oraz możliwością otwierania okienka
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}