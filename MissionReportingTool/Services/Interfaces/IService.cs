using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts;
using MissionReportingTool.Entities;
using MissionReportingTool.Repositories.Interfaces;

namespace MissionReportingTool.Services.Interfaces
{
    public interface IService<T, U> where T : BaseContract where U : BaseCreationRequest
    { 
        Task<long> Create(U request);
        Task<IEnumerable<T>> GetByPaginationRequest(PaginationRequest request);
        Task<T> GetById(long id);
        Task<long> Update(T contract);
        Task Delete(long id);
    }
}
