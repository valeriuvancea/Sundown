using System.ComponentModel.DataAnnotations;

namespace MissionReportingTool.Contracts
{
    public record MissionReport(
        long Id,
        [StringLength(100, MinimumLength = 2)] string Name,
        [StringLength(100, MinimumLength = 2)] string Description,
        double Latitude,
        double Longitude,
        DateTime MissionDate,
        DateTime FinalisationDate,
        long UserId) : BaseContract(Id)
    {
    }
}
