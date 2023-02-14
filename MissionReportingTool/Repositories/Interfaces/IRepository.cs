using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Entities;

namespace MissionReportingTool.Repositories.Interfaces
{
    /// <summary>
    /// This interface can be extended to create interfaces for CRUD repositories
    /// </summary>
    /// <typeparam name="T">The database entity class</typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        Task<long> Create(T entity);
        Task<IEnumerable<T>> GetByPaginationRequest(PaginationRequest request);
        Task<T> GetById(long id);
        Task<long> Update(T entity);
        Task Delete(T entity);
        Task DeleteById(long id);
    }
}
