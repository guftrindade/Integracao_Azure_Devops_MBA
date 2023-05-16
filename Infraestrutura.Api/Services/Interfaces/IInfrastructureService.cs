using Infraestrutura.Api.Models.Request;
using Infraestrutura.Api.Models.Response;

namespace Infraestrutura.Api.Services.Interfaces
{
    public interface IInfrastructureService
    {
        Task<ResponseResource> RequestResource(RequestResource request);
    }
}
