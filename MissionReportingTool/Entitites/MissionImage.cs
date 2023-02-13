namespace MissionReportingTool.Entities
{
    public class MissionImage : BaseEntity
    {
        public string CameraName { get; set; }
        public string RoverName { get; set; }
        public string RoverStatus { get; set; }
        public string Image { get; set; }
        public long MissionReportId { get; set; }
    }
}
