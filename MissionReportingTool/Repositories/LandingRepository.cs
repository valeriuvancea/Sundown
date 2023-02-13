using Microsoft.EntityFrameworkCore;
using MissionReportingTool.Entitites;
using MissionReportingTool.Repositories.Interfaces;

namespace MissionReportingTool.Repositories
{
    public class LandingRepository : ILandingRepository
    {
        private readonly SundownContext Context;
        private readonly DbSet<LandingEntity> Entities;

        public LandingRepository(SundownContext context)
        {
            Context = context;
            this.Entities = Context.Set<LandingEntity>();
        }

        public async Task Add(Facility facility)
        {
            await Entities.AddAsync(
                new LandingEntity
                {
                    Facility = facility,
                    CalculatedAt = DateTime.UtcNow,
                });
            await Context.SaveChangesAsync();
        }

        public async Task<LandingEntity> GetLastClosestLandingPosition()
        {
            return await Entities.OrderByDescending(e => e.CalculatedAt).FirstOrDefaultAsync();
        }
    }
}
