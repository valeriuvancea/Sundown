using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Entities;
using MissionReportingTool.Entitites;
using MissionReportingTool.Helpers;
using MissionReportingTool.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionReportingToolTest.Mocks
{
    public class FakeUserRepository : BaseFakeRepository<UserEntity>, IUserRepository
    {
        public async Task<UserEntity> GetByUsername(string username, long? id = null)
        {
            return Entities.Where(x => x.Username == username && (id == null || x.Id != id)).FirstOrDefault();
        }
    }
}
