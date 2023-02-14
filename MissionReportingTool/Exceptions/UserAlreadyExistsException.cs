using System.Net;

namespace MissionReportingTool.Exceptions
{
    public class UserAlreadyExistsException : BaseHttpResponseException
    {
        public UserAlreadyExistsException(string username) : base(HttpStatusCode.BadRequest, $"User with `{username}` username already exists")
        {
        }
    }
}
