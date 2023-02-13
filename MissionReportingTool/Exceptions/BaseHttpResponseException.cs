using System.Net;

namespace MissionReportingTool.Exceptions
{
    public abstract class BaseHttpResponseException : Exception
    {
        protected BaseHttpResponseException(HttpStatusCode statusCode, string message): base(message) {
            this.StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
