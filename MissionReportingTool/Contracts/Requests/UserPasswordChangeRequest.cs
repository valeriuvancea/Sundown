using System.ComponentModel.DataAnnotations;

namespace MissionReportingTool.Contracts.Requests
{
    public record UserPasswordChangeRequest([StringLength(100, MinimumLength = 2)] string Password)
    {
    }
}
