using MissionReportingTool.Entities;

namespace MissionReportingTool.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task Create(T entity);
        Task<IEnumerable<T>> getAll();
        Task<T> GetById(long id);
        Task Update(T entity);
        Task Delete(T entity);
        Task DeleteById(long id);
    }
}
