using MissionReportingTool.Entitites;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MissionReportingTool.Contracts.Requests
{
    public record UserCreationRequest(
        [StringLength(100, MinimumLength = 2)] string FirstName,
        [StringLength(100, MinimumLength = 2)] string LastName,
        [StringLength(100, MinimumLength = 2)] string CodeName,
        [StringLength(100, MinimumLength = 2)] string Username,
        [StringLength(100, MinimumLength = 2)] string Email,
        [StringLength(100, MinimumLength = 2)] string Password,
        [StringLength(500, MinimumLength = 2)] string Avatar,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] Role Role) : BaseCreationRequest
    {
    }
}
