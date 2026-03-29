using System.Threading.Tasks;

namespace FleetManager.Services;

public interface IDIalogService
{
    Task<bool> ShowConfirmationDialogAsync(string message);
}