using MissionReportingTool.Entities;

namespace MissionReportingTool.Contracts
{
    public record UserContract(long Id, string FirstName, string LastName, string CodeName, string Username, string Email, string Avatar): BaseContract(Id)
    {
    }
}
