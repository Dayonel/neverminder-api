using Microsoft.AspNetCore.Mvc;
using Neverminder.Core.DTO;
using Neverminder.Core.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace Neverminder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RemindersController : ControllerBase
    {
        private readonly ILogger<RemindersController> _logger;
        private readonly IReminderService _reminderService;
        public RemindersController(ILogger<RemindersController> logger,
            IReminderService reminderService)
        {
            _logger = logger;
            _reminderService = reminderService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromHeader(Name = "x-push-token")][Required] string pushToken, ReminderDTO model)
        {
            try
            {
                return await _reminderService.AddReminder(pushToken, model)
                    ? Ok()
                    : BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> Get([FromHeader(Name = "x-push-token")][Required] string pushToken, int id)
        {
            try
            {
                var reminder = await _reminderService.GetReminder(pushToken, id);
                return reminder != null
                    ? Ok(reminder)
                    : BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet, Route("list")]
        public async Task<IActionResult> List([FromHeader(Name = "x-push-token")][Required] string pushToken, [Range(1, int.MaxValue)] int page, [Range(10, 100)] int pageSize)
        {
            try
            {
                var reminders = await _reminderService.ListReminders(pushToken, page, pageSize);
                return reminders != null
                    ? Ok(reminders)
                    : BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> Put([FromHeader(Name = "x-push-token")][Required] string pushToken, int id, ReminderDTO model)
        {
            try
            {
                return await _reminderService.UpdateReminder(pushToken, id, model)
                    ? Ok()
                    : BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Delete([FromHeader(Name = "x-push-token")][Required] string pushToken, int id)
        {
            try
            {
                return await _reminderService.DeleteReminder(pushToken, id)
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
