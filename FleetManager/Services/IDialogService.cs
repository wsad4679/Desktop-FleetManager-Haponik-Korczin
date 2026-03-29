using System.Threading.Tasks;

namespace FleetManager.Services;

public interface IDialogService
{
    Task<bool> ShowConfirmationDialogAsync(string message);
}