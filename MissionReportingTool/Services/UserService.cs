using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts;
using MissionReportingTool.Entities;
using MissionReportingTool.Repositories;
using MissionReportingTool.Repositories.Interfaces;
using MissionReportingTool.Services.Interfaces;
using MissionReportingTool.Exceptions;

namespace MissionReportingTool.Services
{
    public class UserService : BaseService<User, UserEntity, IUserRepository, UserCreationRequest>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {
        }

        public async Task ChangePassword(long id, string password)
        {
            var entity = await GetEntityById(id);
            if (entity.Password == password)
            {
                throw new SamePasswordException();
            }
            entity.Password = password;
            await Repository.Update(entity);
        }

        public async Task<User> GetByUsernameAndPassword(AuthenticateRequest request)
        {
            var entity = await Repository.GetByUsernameAndPassword(request.Username, request.Password);
            return entity != null ? EntityToContract(entity) : null;
        }

        protected override UserEntity CreationRequestToEntity(UserCreationRequest request)
        {
            return new UserEntity
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

        protected override User EntityToContract(UserEntity entity)
        {
            return new User(
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
