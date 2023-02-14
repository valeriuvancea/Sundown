using Microsoft.EntityFrameworkCore;
using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Entities;
using MissionReportingTool.Repositories.Interfaces;
using System.Collections.Generic;

namespace MissionReportingTool.Repositories
{
    /// <summary>
    /// This class can be extended to create a CRUD repository. It already comes with the basic functions
    /// </summary>
    /// <typeparam name="T">The database entity class</typeparam>
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly SundownContext Context;
        protected readonly DbSet<T> Entities;

        protected BaseRepository(SundownContext sundownContext)
        {
            this.Context = sundownContext;
            this.Entities = Context.Set<T>();
        }

        public async Task<long> Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entity.CreatedAt = DateTime.UtcNow;
            await Entities.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entity.DeletedAt = DateTime.UtcNow;
            await Update(entity);
        }

        public async Task DeleteById(long id)
        {
            await Delete(await GetById(id));
        }

        public async Task<IEnumerable<T>> GetByPaginationRequest(PaginationRequest request)
        {
            return await Entities.Where(e => e.Id > request.LastId).Take(request.Limit).ToListAsync();
        }

        public async Task<T> GetById(long id)
        {
            return await Entities.OrderBy(e => e.Id).FirstOrDefaultAsync(entity => entity.Id == id && entity.DeletedAt == DateTime.MinValue);
        }

        public async Task<long> Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entity.UpdatedAt = DateTime.UtcNow;
            Entities.Update(entity);
            await Context.SaveChangesAsync();
            return entity.Id;
        }
    }
}
