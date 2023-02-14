using MissionReportingTool.Contracts;
using MissionReportingTool.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionReportingToolTest.Mocks
{
    public class FakeEntity : BaseEntity
    {
        public string Name { get; set; }

        public override void Update(BaseContract contract)
        {
            var fakeContract = contract as FakeContract;
            if (fakeContract != null)
            {
                Name = fakeContract.Name;
            }
        }
    }
}
