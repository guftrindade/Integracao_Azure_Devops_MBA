using Devops.ViewModels.Infrastructure.Request;

namespace Devops.RabbitServices.Interfaces
{
    public interface IRabbitMessageService
    {
        void Send(RequestResource message);
    }
}
