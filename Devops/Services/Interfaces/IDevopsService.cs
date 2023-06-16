using Devops.ViewModels.Devops.Request;
using Devops.ViewModels.Devops.Response;

namespace Devops.Services.Interfaces
{
    public interface IDevopsService
    {
        Task<ResponseAllProjects> GetAllProjectsAsync();
        Task<ResponseRepository> CreateRepositoryAsync(RequestRepository request);
    }
}
