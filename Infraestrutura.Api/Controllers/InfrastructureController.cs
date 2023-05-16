using Infraestrutura.Api.Models.Request;
using Infraestrutura.Api.Models.Response;
using Infraestrutura.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Infraestrutura.Api.Controllers
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
            var response = _infrastructureService.RequestResource(request);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }
    }
}
