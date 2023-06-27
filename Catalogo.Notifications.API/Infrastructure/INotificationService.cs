using SendGrid;

namespace Catalogo.Notifications.API.Infrastructure
{
    public interface INotificationService
    {
        Task<Response> Send(IEmailTemplate template);
    }
}
