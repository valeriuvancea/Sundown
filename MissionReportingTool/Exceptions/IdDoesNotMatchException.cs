using System.Net;

namespace MissionReportingTool.Exceptions
{
    public class IdDoesNotMatchException : BaseHttpResponseException
    {
        public IdDoesNotMatchException() : base(HttpStatusCode.BadRequest, "The path id and the body id do not match")
        {
        }
    }
}
