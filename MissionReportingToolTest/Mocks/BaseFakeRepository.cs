using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Entities;
using MissionReportingTool.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionReportingToolTest.Mocks
{
    public abstract class BaseFakeRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly List<T> Entities = new List<T>();
        private long Id = 1;

        public virtual async Task<long> Create(T entity)
        {
            entity.Id = Id++;
            Entities.Add(entity);
            return entity.Id;
        }

        public virtual async Task Delete(T entity)
        {
            Entities.Remove(entity);
        }

        public virtual async Task DeleteById(long id)
        {
            Delete(await GetById(id));
        }

        public virtual async Task<T> GetById(long id)
        {
            return Entities.Where(x => x.Id == id).FirstOrDefault();
        }

        public virtual async Task<IEnumerable<T>> GetByPaginationRequest(PaginationRequest request)
        {
            return Entities.OrderBy(x => x.Id).Where(x => x.Id > request.LastId).Take(request.Limit).ToList();
        }

        public virtual async Task<long> Update(T entity)
        {
            Entities[Entities.Select((item, index) => (item, index)).Where(x => x.item.Id == entity.Id).FirstOrDefault().index] = entity;
            return entity.Id;
        }
    }
}
