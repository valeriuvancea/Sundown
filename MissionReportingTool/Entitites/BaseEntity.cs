using Microsoft.EntityFrameworkCore.Update.Internal;
using MissionReportingTool.Contracts;

namespace MissionReportingTool.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public virtual void Update<T>(T contract) where T : BaseContract
        {
            Id = contract.Id;
        }
    }
}
