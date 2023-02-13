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

        public async virtual Task<long> Create(X request)
        {
            return await Repository.Create(CreationRequestToEntity(request));
        }

        public async virtual Task Delete(long id)
        {
            await GetEntityById(id);
            await Repository.DeleteById(id);
        }

        public async virtual Task<IEnumerable<T>> GetByPaginationRequest(PaginationRequest request)
        {
            return (await Repository.GetByPaginationRequest(request)).Select(EntityToContract);
        }

        public async virtual Task<T> GetById(long id)
        {
            return EntityToContract(await GetEntityById(id));
        }

        public async virtual Task<long> Update(T contract)
        {
            var entity = await GetEntityById(contract.Id);
            entity.Update(contract);
            return await Repository.Update(entity);
        }

        protected async virtual Task<U> GetEntityById(long id)
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
