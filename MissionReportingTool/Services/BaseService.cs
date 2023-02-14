using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts;
using MissionReportingTool.Entities;
using MissionReportingTool.Exceptions;
using MissionReportingTool.Repositories.Interfaces;
using MissionReportingTool.Services.Interfaces;

namespace MissionReportingTool.Services
{
    /// <summary>
    /// This class can be extended to create a CRUD service. It already comes with the basic functions
    /// </summary>
    /// <typeparam name="T">The contract returned to the client which is also used as an update request</typeparam>
    /// <typeparam name="U">The database entity class</typeparam>
    /// <typeparam name="V">The repository class used by this server</typeparam>
    /// <typeparam name="X">The creation request contract</typeparam>
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

        public async virtual Task<List<T>> GetByPaginationRequest(PaginationRequest request)
        {
            return (await Repository.GetByPaginationRequest(request)).Select(EntityToContract).ToList();
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

        /// <summary>
        /// Maps a database entity to a client contract
        /// </summary>
        protected abstract T EntityToContract(U entity);

        /// <summary>
        /// Maps a creation request contract to a database entity
        /// </summary>
        protected abstract U CreationRequestToEntity(X request);
    }
}
