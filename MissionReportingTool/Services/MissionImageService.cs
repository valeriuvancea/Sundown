using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts;
using MissionReportingTool.Entities;
using MissionReportingTool.Repositories.Interfaces;
using MissionReportingTool.Services.Interfaces;

namespace MissionReportingTool.Services
{
    public class MissionImageService : BaseService<MissionImage, MissionImageEntity, IMissionImageRepository, MissionImageCreationRequest>, IMissionImageService
    {
        private readonly IMissionReportService MissionReportService;
        public MissionImageService(IMissionImageRepository repository, IMissionReportService missionReportService) : base(repository)
        {
            MissionReportService = missionReportService;
        }

        public override async Task<long> Create(MissionImageCreationRequest request)
        {
            await MissionReportService.GetById(request.MissionReportId);
            return await base.Create(request);
        }

        public override async Task<long> Update(MissionImage contract)
        {
            await MissionReportService.GetById(contract.MissionReportId);
            return await base.Update(contract);
        }

        protected override MissionImageEntity CreationRequestToEntity(MissionImageCreationRequest request)
        {
            return new MissionImageEntity
            {
                CameraName = request.CameraName,
                RoverName = request.RoverName,
                RoverStatus = request.RoverStatus,
                Image = request.Image,
                MissionReportId = request.MissionReportId
            };
        }

        protected override MissionImage EntityToContract(MissionImageEntity entity)
        {
            return new MissionImage(
                entity.Id,
                entity.CameraName,
                entity.RoverName,
                entity.RoverStatus,
                entity.Image,
                entity.MissionReportId
                );
        }
    }
}
