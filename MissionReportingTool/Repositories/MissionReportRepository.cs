using MissionReportingTool.Entities;
using MissionReportingTool.Repositories.Interfaces;

namespace MissionReportingTool.Repositories
{
    public class MissionReportRepository : BaseRepository<MissionReportEntity>, IMissionReportRepository
    {
        public MissionReportRepository(SundownContext sundownContext) : base(sundownContext)
        {
        }
    }
}
