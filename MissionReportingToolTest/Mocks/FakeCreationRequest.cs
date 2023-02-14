using MissionReportingTool.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionReportingToolTest.Mocks
{
    public record FakeCreationRequest(string Name) : BaseCreationRequest
    {
    }
}
