using Microsoft.EntityFrameworkCore;
using MissionReportingTool.Entities;
using MissionReportingTool.Repositories.Interfaces;
using System.Collections.Generic;

namespace MissionReportingTool.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SundownContext Context;
        private readonly DbSet<T> Entities;

        protected BaseRepository(SundownContext sundownContext)
        {
            this.Context = sundownContext;
            this.Entities = Context.Set<T>();
        }

        public async Task Create(T entity)
        {
            await Entities.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task delete(T entity)
        {
            if (entity == null)
            {
                return;
            }
            Entities.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteById(long id)
        {
            await delete(await Entities.FindAsync(id));
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> getAll()
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetById(long id)
        {
            return await Entities.FindAsync(id);
        }

        public async Task Update(T entity)
        {
            if (entity == null)
            {
                return;
            }
            Entities.Update(entity);
            await Context.SaveChangesAsync();
        }
    }
}
