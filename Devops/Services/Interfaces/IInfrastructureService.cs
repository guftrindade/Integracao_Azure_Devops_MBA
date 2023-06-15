using Devops.ViewModels.Infrastructure.Request;
using Devops.ViewModels.Infrastructure.Response;

namespace Devops.Services.Interfaces
{
    public interface IInfrastructureService
    {
        void RequestResource(RequestResource request);
    }
}
