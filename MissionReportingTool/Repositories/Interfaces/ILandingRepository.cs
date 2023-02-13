using MissionReportingTool.Entitites;

namespace MissionReportingTool.Repositories.Interfaces
{
    public interface ILandingRepository
    {
        Task Add(Facility facility);
        Task<LandingEntity> GetLastClosestLandingPosition();
    }
}
