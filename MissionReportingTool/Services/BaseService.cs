using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts;
using MissionReportingTool.Entities;
using MissionReportingTool.Exceptions;
using MissionReportingTool.Repositories.Interfaces;
using MissionReportingTool.Services.Interfaces;

namespace MissionReportingTool.Services
{
    public abstract class BaseService<T, U, V, X> : IService<T, X> where T : BaseContract where U : BaseEntity where V : IRepository<U> where X : BaseCreationRequest
    {
        protected readonly V Repository;

        protected BaseService(V repository)
        {
            Repository = repository;
        }

        public async Task Create(X request)
        {
            await Repository.Create(CreationRequestToEntity(request));
        }

        public async Task Delete(long id)
        {
            await GetEntityById(id);
            await Repository.DeleteById(id);
        }

        public Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetById(long id)
        {
            return EntityToContract(await GetEntityById(id));
        }

        public async Task Update(T contract)
        {
            var entity = await GetEntityById(contract.Id);
            entity.Update(contract);
            await Repository.Update(entity);
        }

        protected async Task<U> GetEntityById(long id)
        {
            var entity = await Repository.GetById(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(T).Name, id);
            }
            return entity;
        }

        protected abstract T EntityToContract(U entity);

        protected abstract U CreationRequestToEntity(X request);
    }
}
