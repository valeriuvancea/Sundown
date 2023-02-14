using MissionReportingTool.Entitites;
using MissionReportingTool.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionReportingToolTest.Mocks
{
    public class FakeLandingRepository : ILandingRepository
    {
        private long Id = 1;
        private readonly List<LandingEntity> Entities = new List<LandingEntity>();

        public async Task Add(Facility facility)
        {
            Entities.Add(new LandingEntity
            {
                Id = Id++,
                Facility = facility,
                CalculatedAt = DateTime.Now,
            });
        }

        public async Task<LandingEntity> GetLastClosestLandingPosition()
        {
            return Entities.OrderByDescending(e => e.CalculatedAt).FirstOrDefault();
        }

        public void Clean()
        {
            Entities.Clear();
        }
    }
}
