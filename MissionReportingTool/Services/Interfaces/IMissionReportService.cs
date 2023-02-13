using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts;
using MissionReportingTool.Entities;

namespace MissionReportingTool.Services.Interfaces
{
    public interface IMissionReportService : IService<MissionReport, MissionReportCreationRequest>
    {
    }
}
