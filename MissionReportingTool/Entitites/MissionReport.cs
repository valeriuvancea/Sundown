using System.ComponentModel.DataAnnotations.Schema;

namespace MissionReportingTool.Entities
{
    public class MissionReport: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime MissionDate { get; set; }
        public DateTime FinalisationDate { get; set; }
        public long UserId { get; set; }
    }
}
