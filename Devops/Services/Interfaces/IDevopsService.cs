using Devops.ViewModels.Devops.Request;
using Devops.ViewModels.Devops.Response;

namespace Devops.Services.Interfaces
{
    public interface IDevopsService
    {
        Task<ResponseAllProjects> GetAllProjectsAsync();
        Task<ResponseResource> CreateRepositoryAsync(RequestRepository request);
    }
}
