using Microsoft.AspNetCore.Mvc;
using MissionReportingTool.Contracts;
using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts.Responses;
using MissionReportingTool.Exceptions;
using MissionReportingTool.Services;
using MissionReportingTool.Services.Interfaces;

namespace MissionReportingTool.Controllers
{
    public class MissionReportController : BaseApiController<MissionReport, MissionReportCreationRequest, IMissionReportService>
    {
        private readonly IMissionImageService MissionImageService;

        public MissionReportController(IMissionReportService service, IMissionImageService missionImageService) : base(service)
        {
            MissionImageService = missionImageService;
        }

        [HttpPost("{id}/MissionImage")]
        public async Task<IActionResult> CreateMissionImage(long id, MissionImageCreationRequest request)
        {
            if (id != request.MissionReportId)
            {
                throw new IdDoesNotMatchException();
            }
            return Json(new IdResponse(await MissionImageService.Create(request)));
        }
    }
}
