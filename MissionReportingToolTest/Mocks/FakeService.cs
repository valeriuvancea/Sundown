using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Services;
using MissionReportingTool.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionReportingToolTest.Mocks
{
  public class FakeService : BaseService<FakeContract, FakeEntity, FakeRepository, FakeCreationRequest>, IService<FakeContract, FakeCreationRequest>
  {
        public FakeService(FakeRepository repository) : base(repository)
        {
        }

        protected override FakeEntity CreationRequestToEntity(FakeCreationRequest request)
        {
        return new FakeEntity {
                Name = request.Name
        };
        }

        protected override FakeContract EntityToContract(FakeEntity entity)
        {
        return new FakeContract(entity.Id, entity.Name);
        }
  }
}
