using Devops.RabbitServices.Interfaces;
using Devops.Services.Interfaces;
using Devops.ViewModels.Infrastructure.Request;
using Devops.ViewModels.Infrastructure.Response;

namespace Devops.Services
{
    public class InfrastructureService : IInfrastructureService
    {
        private readonly IRabbitMessageService _rabbitMessageService;

        public InfrastructureService(IRabbitMessageService rabbitMessageService)
        {
            _rabbitMessageService = rabbitMessageService;
        }

        public async Task<ResponseResource> RequestResource(RequestResource request)
        {
            _rabbitMessageService.Send(request);

            return new ResponseResource
            {
                Status = "Temp",
                ProtocolNumber = 15468,
                Note = "Temp"
            };
        }
    }
}
