using MissionReportingTool.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionReportingToolTest.Mocks
{
    public record FakeContract(long Id, string Name) : BaseContract(Id)
    {
    }
}
