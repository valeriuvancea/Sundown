using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts;
using MissionReportingTool.Services.Interfaces;
using MissionReportingTool.Exceptions;
using MissionReportingTool.Contracts.Responses;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MissionReportingTool.Entitites;
using MissionReportingTool.Authorization;

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
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create(UserCreationRequest request)
        {
            return Json(new IdResponse(await Service.Create(request)));
        }


        [HttpGet("{id}")]
        [AdminOrCurrentUserAuthorization]
        public override async Task<IActionResult> GetById(long id)
        {
            return await base.GetById(id);
        }

        [HttpPost("{id}/MissionReport")]
        [Authorize(Roles = "ASTRONAUT")]
        public async Task<IActionResult> CreateMissionReport(long id, MissionReportCreationRequest request)
        {
            if (id != request.UserId)
            {
                throw new IdDoesNotMatchException();
            }
            return Json(new IdResponse(await MissionReportService.Create(request)));
        }

        [HttpPost("{id}/ChangePassword")]
        [AdminOrCurrentUserAuthorization]
        public async Task ChangePassword(long id, UserPasswordChangeRequest request)
        {
            await Service.ChangePassword(id, request.Password);
        }



        [HttpPut("{id}")]
        [AdminOrCurrentUserAuthorization]
        public override async Task<IActionResult> Update(long id, User contract)
        {
            return await base.Update(id, contract);
        }


        [HttpDelete("{id}")]
        [AdminOrCurrentUserAuthorization]
        public override async Task Delete(long id)
        {
            await base.Delete(id);
        }
    }
}
