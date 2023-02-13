using System.ComponentModel.DataAnnotations;

namespace MissionReportingTool.Contracts.Requests
{
    public record AuthenticateRequest([StringLength(100, MinimumLength = 2)] string Username, [StringLength(100, MinimumLength = 2)] String Password)
    {
    }
}
