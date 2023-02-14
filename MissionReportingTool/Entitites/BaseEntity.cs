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

        /// <summary>
        /// This method updates the fields of the entity with values from the contract. The BaseContract can be casted to the specific contract to get access to all the fields. The method is used during an update request.
        /// </summary>
        /// <param name="contract"></param>
        public abstract void Update(BaseContract contract);
    }
}
