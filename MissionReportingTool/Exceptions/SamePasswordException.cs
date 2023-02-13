using System.Net;

namespace MissionReportingTool.Exceptions
{
    public class SamePasswordException : BaseHttpResponseException
    {
        public SamePasswordException() : base(HttpStatusCode.BadRequest, "The new password should not be the same as the old one")
        {
        }
    }
}
