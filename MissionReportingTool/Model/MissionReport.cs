namespace MissionReportingTool.Model
{
    public class MissionReport: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime MissionDate { get; set; }
        public DateTime FinalisationDate { get; set; }
        public User User { get; set; }
        public ICollection<MissionImage> MissionImages { get; set; }
    }
}
