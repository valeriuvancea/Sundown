
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Services.Interfaces;

namespace MissionReportingTool.Controllers
{
    [AllowAnonymous]
    [Route("Api/[controller]")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private readonly IAuthenticationService AuthenticationService;

        public AuthenticateController(IAuthenticationService authenticationService, ILogger<AuthenticateController> logger)
        {
            this.AuthenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthenticateRequest request)
        {
            return Json(await AuthenticationService.Authenticate(request));
        }
    }
}
