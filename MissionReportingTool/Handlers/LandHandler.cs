using MissionReportingTool.Delegates.EventsArgs;
using MissionReportingTool.Entitites;

namespace MissionReportingTool.Handlers
{
    public class LandHandler
    {
        private readonly ILogger<LandHandler> Logger;

        public LandHandler(ILogger<LandHandler> logger)
        {
            this.Logger = logger;
        }

        public void Handle(LandedEventArgs eventArgs)
        {
            Logger.LogInformation("Landed requested at `{}` in `{}`", eventArgs.LandingTime, eventArgs.Facility);
        }
    }
}
