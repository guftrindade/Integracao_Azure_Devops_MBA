using Devops.ViewModels.Infrastructure.Request;

namespace Devops.RabbitServices.Interfaces
{
    public interface IRabbitMqService
    {
        void Publish(RequestResource message);
    }
}
