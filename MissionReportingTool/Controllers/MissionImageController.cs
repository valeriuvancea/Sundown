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
    }
}
