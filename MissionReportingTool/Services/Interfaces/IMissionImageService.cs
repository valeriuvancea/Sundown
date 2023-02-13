using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts;

namespace MissionReportingTool.Services.Interfaces
{
    public interface IMissionImageService : IService<MissionImage, MissionImageCreationRequest>
    {
    }
}
