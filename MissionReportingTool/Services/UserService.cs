using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts;
using MissionReportingTool.Entities;
using MissionReportingTool.Repositories;
using MissionReportingTool.Repositories.Interfaces;
using MissionReportingTool.Services.Interfaces;

namespace MissionReportingTool.Services
{
    public class UserService : BaseService<UserContract, User, IUserRepository, UserCreationRequest>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {
        }

        protected override User CreationRequestToEntity(UserCreationRequest request)
        {
            return new User()
            {
                Avatar = request.Avatar,
                CodeName = request.CodeName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                Password = request.Password,
            };
        }

        protected override UserContract EntityToContract(User entity)
        {
            return new UserContract(
                entity.Id,
                entity.FirstName,
                entity.LastName,
                entity.CodeName,
                entity.Username,
                entity.Email,
                entity.Avatar);
        }
    }
}
