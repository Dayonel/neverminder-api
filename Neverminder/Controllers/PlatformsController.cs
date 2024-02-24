using Microsoft.AspNetCore.Mvc;
using Neverminder.Core.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace Neverminder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly ILogger<PlatformsController> _logger;
        private readonly IPlatformService _platformService;
        public PlatformsController(ILogger<PlatformsController> logger,
            IPlatformService platformService)
        {
            _logger = logger;
            _platformService = platformService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromHeader(Name = "x-push-token")][Required] string pushToken)
        {
            try
            {
                return await _platformService.Post(pushToken)
                    ? Ok()
                    : BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
