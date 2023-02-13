using System.ComponentModel.DataAnnotations;

namespace MissionReportingTool.Contracts
{
    public record MissionImage(
        long Id,
        [StringLength(100, MinimumLength = 2)] string CameraName,
        [StringLength(100, MinimumLength = 2)] string RoverName,
        [StringLength(100, MinimumLength = 2)] string RoverStatus,
        [StringLength(1000, MinimumLength = 1)] string Image,
        long MissionReportId) : BaseContract(Id)
    {
    }
}
