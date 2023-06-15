using Devops.RabbitServices.Interfaces;
using Devops.Services.Interfaces;
using Devops.ViewModels.Infrastructure.Request;
using Devops.ViewModels.Infrastructure.Response;

namespace Devops.Services
{
    public class InfrastructureService : IInfrastructureService
    {
        private readonly IRabbitMqService _messageBus;
        //To do: private readonly IRepository _repository;

        public InfrastructureService(IRabbitMqService rabbitMessageService)
        {
            _messageBus = rabbitMessageService;
        }

        public void RequestResource(RequestResource request)
        {
            _messageBus.Publish(request);
        }
    }
}
