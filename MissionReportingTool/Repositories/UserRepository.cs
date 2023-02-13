using MissionReportingTool.Entities;
using MissionReportingTool.Repositories.Interfaces;

namespace MissionReportingTool.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(SundownContext sundownContext) : base(sundownContext)
        {
        }
    }
}
