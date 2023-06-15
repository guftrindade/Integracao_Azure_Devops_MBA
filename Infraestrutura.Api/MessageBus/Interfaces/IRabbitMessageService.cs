

using Infraestrutura.Api.Models.Request;

namespace Infraestrutura.Api.RabbitServices.Interfaces
{
    public interface IRabbitMessageService
    {
        void Receive(RequestResource message);
    }
}
