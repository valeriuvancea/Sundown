using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts;
using MissionReportingTool.Entities;
using MissionReportingTool.Repositories;
using MissionReportingTool.Repositories.Interfaces;
using MissionReportingTool.Services.Interfaces;
using MissionReportingTool.Exceptions;
using MissionReportingTool.Helpers;

namespace MissionReportingTool.Services
{
    public class UserService : BaseService<User, UserEntity, IUserRepository, UserCreationRequest>, IUserService
    {
        private readonly string RandomHashSalt;
        public UserService(IUserRepository repository, IConfiguration configuration) : base(repository)
        {
            RandomHashSalt = configuration["RandomHashSalt"];
        }

        public async override Task<long> Create(UserCreationRequest request)
        {
            var existingUser = await Repository.GetByUsername(request.Username);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException(request.Username);
            }
            return await base.Create(request with { Password = PasswordHelper.Hash(request.Password) });
        }

        public async override Task<long> Update(User contract)
        {
            var existingUser = await Repository.GetByUsername(contract.Username, contract.Id);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException(contract.Username);
            }
            return await base.Update(contract);
        }

        public async Task ChangePassword(long id, string password)
        {
            var entity = await GetEntityById(id);
            if (entity.Password == password)
            {
                throw new SamePasswordException();
            }
            entity.Password = PasswordHelper.Hash(password);
            await Repository.Update(entity);
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
                Role = request.Role
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
                entity.Avatar,
                entity.Role);
        }
    }
}
