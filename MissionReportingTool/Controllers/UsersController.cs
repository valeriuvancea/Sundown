using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts;
using MissionReportingTool.Services.Interfaces;
using MissionReportingTool.Exceptions;
using MissionReportingTool.Contracts.Responses;

namespace MissionReportingTool.Controllers
{
    public class UsersController : BaseApiController<User, UserCreationRequest, IUserService>
    {
        private readonly IMissionReportService MissionReportService;

        public UsersController(IUserService userService, IMissionReportService missionReportService) : base(userService)
        {
            MissionReportService = missionReportService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreationRequest request)
        {
            return Json(new IdResponse(await Service.Create(request)));
        }

        [HttpPost("{id}/MissionReport")]
        public async Task<IActionResult> CreateMissionReport(long id, MissionReportCreationRequest request)
        {
            if (id != request.UserId)
            {
                throw new IdDoesNotMatchException();
            }
            return Json(new IdResponse(await MissionReportService.Create(request)));
        }

        [HttpPost("{id}/ChangePassword")]
        public async Task ChangePassword(long id, UserPasswordChangeRequest request)
        {
            await Service.ChangePassword(id, request.Password);
        }
    }
}
