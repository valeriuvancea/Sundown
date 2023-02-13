using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts;
using MissionReportingTool.Entities;
using MissionReportingTool.Repositories.Interfaces;

namespace MissionReportingTool.Services.Interfaces
{
    public interface IService<T, U> where T : BaseContract where U : BaseCreationRequest
    { 
        Task Create(U request);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(long id);
        Task Update(T contract);
        Task Delete(long id);
    }
}
