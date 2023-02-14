using System.Net;

namespace MissionReportingTool.Exceptions
{
    /// <summary>
    /// This class can be extended to create exceptions that will be mapped to HTTP responses with custom status codes
    /// </summary>
    public abstract class BaseHttpResponseException : Exception
    {
        protected BaseHttpResponseException(HttpStatusCode statusCode, string message): base(message) {
            this.StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
