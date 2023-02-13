using System.ComponentModel.DataAnnotations;

namespace MissionReportingTool.Contracts.Requests
{
    public record MissionImageCreationRequest(
        [StringLength(100, MinimumLength = 2)] string CameraName,
        [StringLength(100, MinimumLength = 2)] string RoverName,
        [StringLength(100, MinimumLength = 2)] string RoverStatus,
        [StringLength(1000, MinimumLength = 1)] string Image,
        long MissionReportId) : BaseCreationRequest
    {
    }
}
