using MissionReportingTool.Entities;
using MissionReportingTool.Repositories.Interfaces;

namespace MissionReportingTool.Repositories
{
    public class MissionImageRepository : BaseRepository<MissionImageEntity>, IMissionImageRepository
    {
        public MissionImageRepository(SundownContext sundownContext) : base(sundownContext)
        {
        }
    }
}
