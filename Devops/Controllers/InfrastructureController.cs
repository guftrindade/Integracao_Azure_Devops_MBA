
using Devops.Services.Interfaces;
using Devops.ViewModels.Infrastructure.Request;
using Devops.ViewModels.Infrastructure.Response;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Devops.Controllers
{
    [Route("api/resource")]
    [ApiController]
    [OpenApiTag("Resource", Description = "Serviços de Recursos para Portal do Azure")]
    public class InfrastructureController : Controller
    {
        private readonly IInfrastructureService _infrastructureService;

        public InfrastructureController(IInfrastructureService infrastructureService)
        {
            _infrastructureService = infrastructureService;
        }

        [HttpPost("azure-resource")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ResponseResource> RequestResource([FromBody] RequestResource request)
        {
            _infrastructureService.RequestResource(request);

            var response = new ResponseResource();

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }
    }
}
