using Devops.ViewModels.Infrastructure.Request;

namespace Devops.Services.Interfaces
{
    public interface IInfrastructureService
    {
        void RequestResource(RequestResource request);
    }
}
