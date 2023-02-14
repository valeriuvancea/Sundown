using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MissionReportingTool.Contracts;
using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts.Responses;
using MissionReportingTool.Exceptions;
using MissionReportingTool.Services;
using MissionReportingTool.Services.Interfaces;

namespace MissionReportingTool.Controllers
{
    public class MissionReportController : BaseCrudController<MissionReport, MissionReportCreationRequest, IMissionReportService>
    {
        private readonly IMissionImageService MissionImageService;

        public MissionReportController(IMissionReportService service, IMissionImageService missionImageService) : base(service)
        {
            MissionImageService = missionImageService;
        }

        [HttpPost("{id}/MissionReport")]
        [Authorize(Roles = "ASTRONAUT")]
        public override async Task<IActionResult> Create(MissionReportCreationRequest request)
        {
            return await base.Create(request);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ASTRONAUT")]
        public override async Task<IActionResult> Update(long id, MissionReport contract)
        {
            return await base.Update(id, contract);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ASTRONAUT")]
        public override async Task Delete(long id)
        {
            await base.Delete(id);
        }
    }
}
