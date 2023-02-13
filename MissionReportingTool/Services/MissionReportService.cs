using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts;
using MissionReportingTool.Entities;
using MissionReportingTool.Repositories.Interfaces;
using MissionReportingTool.Services.Interfaces;

namespace MissionReportingTool.Services
{
    public class MissionReportService : BaseService<MissionReport, MissionReportEntity, IMissionReportRepository, MissionReportCreationRequest>, IMissionReportService
    {
        private readonly IUserService UserService;

        public MissionReportService(IMissionReportRepository repository, IUserService userService) : base(repository)
        {
            this.UserService = userService;
        }

        public override async Task<long> Create(MissionReportCreationRequest request)
        {
            await UserService.GetById(request.UserId);
            return await base.Create(request);
        }

        public override async Task<long> Update(MissionReport contract)
        {
            await UserService.GetById(contract.UserId);
            return await base.Update(contract);
        }

        protected override MissionReportEntity CreationRequestToEntity(MissionReportCreationRequest request)
        {
            return new MissionReportEntity
            {
                Name = request.Name,
                Description = request.Description,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                MissionDate = request.MissionDate,
                FinalisationDate = request.FinalisationDate, 
                UserId = request.UserId,
            };
        }

        protected override MissionReport EntityToContract(MissionReportEntity entity)
        {
            return new MissionReport(
                entity.Id,
                entity.Name,
                entity.Description,
                entity.Latitude,
                entity.Longitude,
                entity.MissionDate,
                entity.FinalisationDate,
                entity.UserId
                );
        }
    }
}
