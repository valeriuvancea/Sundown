using MissionReportingTool.Entitites;

namespace MissionReportingTool.Delegates.EventsArgs
{
    public class LandedEventArgs: EventArgs
    {
        public DateTime LandingTime { get; set; }
        public Facility Facility { get; set; }
    }
}
