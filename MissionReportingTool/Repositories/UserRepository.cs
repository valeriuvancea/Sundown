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

        /// <summary>
        /// Returns the first user with the provided <param>username<param>, and that doesn't have the provided <param>id<param>, if this was not null
        /// </summary>
        public async Task<UserEntity> GetByUsername(string username, long? id = null)
        {
            return await Entities.Where(e => e.Username == username && (id == null || e.Id != id)).FirstOrDefaultAsync();
        }
    }
}
