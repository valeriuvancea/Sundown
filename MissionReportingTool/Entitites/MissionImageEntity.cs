using MissionReportingTool.Contracts;

namespace MissionReportingTool.Entities
{
    public class MissionImageEntity : BaseEntity
    {
        public string CameraName { get; set; }
        public string RoverName { get; set; }
        public string RoverStatus { get; set; }
        public string Image { get; set; }
        public long MissionReportId { get; set; }

        public override void Update(BaseContract contract)
        {
            var missionImage = contract as MissionImage;
            if (missionImage != null)
            {
                CameraName = missionImage.CameraName;
                RoverName = missionImage.RoverName;
                RoverStatus = missionImage.RoverStatus;
                Image = missionImage.Image;
                MissionReportId = missionImage.MissionReportId;
            }
        }
    }
}
