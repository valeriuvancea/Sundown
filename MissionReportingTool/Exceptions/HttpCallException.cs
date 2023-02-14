using System.Net;

namespace MissionReportingTool.Exceptions
{
    public class HttpCallException : BaseHttpResponseException
    {
        public HttpCallException(Exception excpeiton) : base(HttpStatusCode.InternalServerError, $"Exception encountered during HTTP call: {excpeiton.Message}")
        {
        }
    }
}
