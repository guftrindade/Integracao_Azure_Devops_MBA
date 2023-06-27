using Catalogo.Notifications.API.Infrastructure;
using Catalogo.Notifications.API.Interfaces;
using Catalogo.Notifications.API.Models;
using SendGrid;

namespace Catalogo.Notifications.API.Services
{
    public class NotifyService : INotifyService
    {
        private readonly IServiceProvider _serviceProvider;

        public NotifyService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Response> Notify(CreateResourceEmailEvent @event)
        {
            using var scope = _serviceProvider.CreateScope();

            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

            var template = new CreateResourceEmailTemplate(@event.ProtocolNumber, @event.ContactEmail, @event.Description);

            return await notificationService.Send(template);
        }
    }
}
