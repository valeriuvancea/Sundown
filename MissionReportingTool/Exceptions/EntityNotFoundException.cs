using System.Net;

namespace MissionReportingTool.Exceptions
{
    public class EntityNotFoundException : BaseHttpResponseException
    {
        public EntityNotFoundException(string entityName, long id) : base(HttpStatusCode.NotFound, $"{entityName} with id `{id}` not found")
        {
        }
    }
}
