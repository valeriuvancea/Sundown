using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MissionReportingTool.Contracts;
using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Services.Interfaces;

namespace MissionReportingTool.Controllers
{
    public class MissionImageController : BaseApiController<MissionImage, MissionImageCreationRequest, IMissionImageService>
    {
        public MissionImageController(IMissionImageService service) : base(service)
        {
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ASTRONAUT")]
        public override async Task<IActionResult> Update(long id, MissionImage contract)
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
