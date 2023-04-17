
using Devops.ViewModels.Infrastructure.Request;
using Devops.ViewModels.Infrastructure.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using NSwag.Annotations;
using System.Net;
using System.Text;

namespace Devops.Controllers
{
    [Route("api/resource")]
    [ApiController]
    [OpenApiTag("Resource", Description = "Serviços de Recursos para Portal do Azure")]
    public class InfrastructureController : Controller
    {
        [HttpPost("azure-resource")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ResponseResource> RequestResource([FromBody] RequestResource request)
        {
            var response = ServiceRequestResource(request);

            if (response == null)
            {
                return BadRequest();
            }

            return Ok(response);
        }

        public ResponseResource ServiceRequestResource(RequestResource request)
        {
            return new ResponseResource
            {
                Status = "Em processo de aprovação",
                ProtocolNumber = 15468,
                Note = NoteMessage(request.Email)
            };
        }

        private static string NoteMessage(string email)
        {
            var requestTime = DateTime.Now;

            return $"Solicitação enviada para o Setor de Infraestrutura em {requestTime} e " +
                   $"dentro de 3 dias úteis uma resposta será enviada para " +
                   $"{email}. Qualquer outra ponderação, favor entrar em contato.";
        }
    }
}
