using Catalogo.Notifications.API.Models;
using SendGrid;

namespace Catalogo.Notifications.API.Interfaces
{
    public interface INotifyService
    {
        Task<Response> Notify(CreateResourceEmailEvent @event);
    }
}
