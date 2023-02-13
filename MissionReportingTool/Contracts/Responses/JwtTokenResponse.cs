namespace MissionReportingTool.Contracts.Responses
{
    public record JwtTokenResponse(string Token, DateTime validUntil)
    {
    }
}
