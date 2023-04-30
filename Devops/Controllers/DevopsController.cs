using Microsoft.AspNetCore.Mvc;
using Devops.ViewModels.Devops.Request;
using Devops.ViewModels.Devops.Response;
using Devops.Services.Interfaces;

namespace Devops.Controllers
{
    public class DevopsController : Controller
    {
        private readonly IDevopsService _devopsService;

        public DevopsController(IDevopsService devopsService)
        {
            _devopsService = devopsService;
        }

        [HttpGet("projects")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ResponseAllProjects>> GetAllProjectsAsync()
        {
            var response = await _devopsService.GetAllProjectsAsync();

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("repository")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResponseResource>> CreateRepositoryAsync([FromBody] RequestRepository request)
        {
            var response = await _devopsService.CreateRepositoryAsync(request);

            if (response == null)
            {
                return BadRequest();
            }

            return Created(nameof(GetAllProjectsAsync), response);
        }
    }
}
