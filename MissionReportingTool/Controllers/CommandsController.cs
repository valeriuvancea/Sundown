using Microsoft.AspNetCore.Mvc;
using MissionReportingTool.Services.Interfaces;

namespace MissionReportingTool.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class CommandsController : Controller
    {
        private readonly ICommandsService CommandsService;

        public CommandsController(ICommandsService commandsService)
        {
            CommandsService = commandsService;
        }

        [HttpPost("Land")]
        public async Task Land()
        {
            await CommandsService.Land();
        }
    }
}
