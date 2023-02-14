using Microsoft.EntityFrameworkCore;
using MissionReportingTool.Entities;
using MissionReportingTool.Repositories.Interfaces;

namespace MissionReportingTool.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(SundownContext sundownContext) : base(sundownContext)
        {
        }

        public async Task<UserEntity> GetByUsername(string username, long? id = null)
        {
            return await Entities.Where(e => e.Username == username && (id == null || e.Id != id)).FirstOrDefaultAsync();
        }
    }
}
