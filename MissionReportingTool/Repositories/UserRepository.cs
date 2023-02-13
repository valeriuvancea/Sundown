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

        public async Task<UserEntity> GetByUsernameAndPassword(string username, string password)
        {
            return await Entities.Where(e => e.Username == username && e.Password == password).FirstOrDefaultAsync();
        }
    }
}
