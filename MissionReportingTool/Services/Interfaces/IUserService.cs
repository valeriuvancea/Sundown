using MissionReportingTool.Contracts;
using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Entities;

namespace MissionReportingTool.Services.Interfaces
{
    public interface IUserService : IService<User, UserCreationRequest>
    {
        Task ChangePassword(long id, string password);
    }
}
