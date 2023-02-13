using MissionReportingTool.Contracts;
using MissionReportingTool.Contracts.Requests;

namespace MissionReportingTool.Services.Interfaces
{
    public interface IUserService : IService<UserContract, UserCreationRequest>
    {
    }
}
