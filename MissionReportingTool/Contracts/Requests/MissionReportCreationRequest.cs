using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MissionReportingTool.Contracts.Requests
{
    public record MissionReportCreationRequest(
        [StringLength(100, MinimumLength = 2)] string Name,
        [StringLength(100, MinimumLength = 2)] string Description,
        double Latitude,
        double Longitude, 
        DateTime MissionDate,
        DateTime FinalisationDate,
        long UserId) : BaseCreationRequest
    {
    }
}
