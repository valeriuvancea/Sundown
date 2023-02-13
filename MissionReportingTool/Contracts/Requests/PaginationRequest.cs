using System.ComponentModel.DataAnnotations;

namespace MissionReportingTool.Contracts.Requests
{
    public record PaginationRequest([Range(0, long.MaxValue)] long LastId, [Range(1, long.MaxValue)] int Limit)
    {
    }
}
