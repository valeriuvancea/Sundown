using MissionReportingTool.Contracts;
using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts.Responses;

namespace MissionReportingTool.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<JwtTokenResponse> Authenticate(AuthenticateRequest request);
    }
}
