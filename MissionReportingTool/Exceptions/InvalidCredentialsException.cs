using System.Net;

namespace MissionReportingTool.Exceptions
{
    public class InvalidCredentialsException : BaseHttpResponseException
    {
        public InvalidCredentialsException() : base(HttpStatusCode.Unauthorized, "Invalid username or password")
        {
        }
    }
}
