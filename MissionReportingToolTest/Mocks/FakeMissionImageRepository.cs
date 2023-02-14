using MissionReportingTool.Entities;
using MissionReportingTool.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionReportingToolTest.Mocks
{
    public class FakeMissionImageRepository: BaseFakeRepository<MissionImageEntity>, IMissionImageRepository
    {
    }
}
