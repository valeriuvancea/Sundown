using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts;
using MissionReportingTool.Entities;
using MissionReportingTool.Repositories.Interfaces;

namespace MissionReportingTool.Services.Interfaces
{
    /// <summary>
    /// This interface can be extended to create interfaces for CRUD services
    /// </summary>
    /// <typeparam name="T">The contract returned to the client which is also used as an update request</typeparam>
    /// <typeparam name="U">The creation request contract</typeparam>
    public interface IService<T, U> where T : BaseContract where U : BaseCreationRequest
    { 
        Task<long> Create(U request);
        Task<List<T>> GetByPaginationRequest(PaginationRequest request);
        Task<T> GetById(long id);
        Task<long> Update(T contract);
        Task Delete(long id);
    }
}
