using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MissionReportingTool.Services.Interfaces;

namespace MissionReportingTool.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    [Authorize(Roles = "GENERAL,ADMIN")]
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
