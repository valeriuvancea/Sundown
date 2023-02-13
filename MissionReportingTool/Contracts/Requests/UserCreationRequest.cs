namespace MissionReportingTool.Contracts.Requests
{
    public record UserCreationRequest(string FirstName, string LastName, string CodeName, string Username, string Email, string Password, string Avatar): BaseCreationRequest
    {
    }
}
