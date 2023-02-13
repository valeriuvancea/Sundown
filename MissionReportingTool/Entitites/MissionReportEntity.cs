using MissionReportingTool.Contracts;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace MissionReportingTool.Entities
{
    public class MissionReportEntity: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime MissionDate { get; set; }
        public DateTime FinalisationDate { get; set; }
        public long UserId { get; set; }

        public override void Update(BaseContract contract)
        {
            var missionReport = contract as MissionReport;
            if (missionReport != null)
            {
                Name = missionReport.Name;
                Description = missionReport.Description;
                Latitude = missionReport.Latitude;
                Longitude = missionReport.Longitude;
                MissionDate = missionReport.MissionDate;
                FinalisationDate = missionReport.FinalisationDate;
                UserId = missionReport.UserId;
            }
        }
    }
}
