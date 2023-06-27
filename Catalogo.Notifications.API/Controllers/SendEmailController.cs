using Catalogo.Notifications.API.Interfaces;
using Catalogo.Notifications.API.Models;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using SendGrid;

namespace Catalogo.Notifications.API.Controllers
{
    [Route("api/email")]
    [ApiController]
    [OpenApiTag("Email", Description = "Serviços de envio de email")]
    public class SendEmailController : Controller
    {
        private readonly INotifyService _notifyService;

        public SendEmailController(INotifyService notifyService)
        {
            _notifyService = notifyService;
        }

        [HttpPost(Name = "teste")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response>> SendEmail([FromBody] CreateResourceEmailEvent @event)
        {
            var response = await _notifyService.Notify(@event);

            if(!response.IsSuccessStatusCode)
            {
                return BadRequest(response.Body.ReadAsStringAsync());
            }

            return Ok();
        }
    }
}
