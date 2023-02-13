using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Entities;

namespace MissionReportingTool.Repositories.Interfaces
{
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
