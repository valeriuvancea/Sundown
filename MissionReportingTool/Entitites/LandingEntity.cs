namespace MissionReportingTool.Entitites
{
    public class LandingEntity
    {
        public long Id { get; set; }
        public DateTime CalculatedAt { get; set; }
        public Facility Facility { get; set; }
    }
}
