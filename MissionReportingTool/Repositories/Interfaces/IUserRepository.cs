using MissionReportingTool.Entities;

namespace MissionReportingTool.Repositories.Interfaces
{
    public interface IUserRepository: IRepository<UserEntity>
    {
        Task<UserEntity> GetByUsernameAndPassword(string username, string password);
    }
}
