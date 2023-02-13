using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts;
using MissionReportingTool.Services.Interfaces;

namespace MissionReportingTool.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserService UserService;

        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return Json(await UserService.GetById(id));
        }

        [HttpPost]
        public async Task Create(UserCreationRequest request)
        {
            await UserService.Create(request);
        }
    }
}
