using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FleetManager.ViewModels;

namespace FleetManager.Views;

public partial class ConfirmationWindow : Window
{
    public ConfirmationWindow()
    {
        InitializeComponent();
    }
    
    protected override void OnOpened(EventArgs e)
    {
        if (DataContext is not ConfirmationWindowViewModel vm) return;
        vm.ConfirmCommand.Subscribe(result =>
        {
            this.Close(result);
        });

        vm.CancelCommand.Subscribe(result =>
            {
                this.Close(result);
            }
        );
    }
}