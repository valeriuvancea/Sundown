using MissionReportingTool.Entities;

namespace MissionReportingTool.Repositories.Interfaces
{
    public interface IUserRepository: IRepository<UserEntity>
    {

        /// <summary>
        /// Returns the first user with the provided <param>username<param>, and that doesn't have the provided <param>id<param>, if this was not null
        /// </summary>
        Task<UserEntity> GetByUsername(string username, long? id = null);
    }
}
