using MissionReportingTool.Entities;

namespace MissionReportingTool.Repositories.Interfaces
{
    public interface IUserRepository: IRepository<UserEntity>
    {

        /// <summary>
        /// Returns the first user with the provided username, and that doesn't have the provided id, if this was not null
        /// </summary>
        Task<UserEntity> GetByUsername(string username, long? id = null);
    }
}
