using MissionReportingTool.Contracts;
using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Entitites;
using MissionReportingTool.Exceptions;
using MissionReportingTool.Helpers;
using MissionReportingTool.Repositories.Interfaces;
using MissionReportingTool.Services;
using MissionReportingTool.Services.Interfaces;
using MissionReportingToolTest.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionReportingToolTest.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private static readonly IUserRepository UserRepository = new FakeUserRepository();
        private static readonly UserService UserService = new UserService(UserRepository);

        [TestCleanup]
        public async Task clean()
        {
            foreach (var x in await UserService.GetByPaginationRequest(new PaginationRequest(0, 100)))
            {
                await UserService.Delete(x.Id);
            }
        }

        [TestMethod]
        public async Task testCreate()
        {
            var creationRequest = new UserCreationRequest("FirstName", "LastName", "CodeName", "UserName", "Email", "Password", "Avatar", Role.ASTRONAUT);
            var id = await UserService.Create(creationRequest);
            var actual = await UserService.GetById(id);
            var entity = await UserRepository.GetById(id);
            var expected = new User(id, creationRequest.FirstName, creationRequest.LastName, creationRequest.CodeName, creationRequest.Username, creationRequest.Email, creationRequest.Avatar, creationRequest.Role);
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(PasswordHelper.Check(entity.Password, creationRequest.Password));
        }

        [TestMethod]
        public async Task testCreateThrowsWhenUsernameExists()
        {
            var creationRequest = new UserCreationRequest("FirstName", "LastName", "CodeName", "UserName", "Email", "Password", "Avatar", Role.ASTRONAUT);
            var id = await UserService.Create(creationRequest);
            await Assert.ThrowsExceptionAsync<UserAlreadyExistsException>(async () =>
            {
                await UserService.Create(creationRequest);
            });
        }

        [TestMethod]
        public async Task testUpdate()
        {
            var creationRequest = new UserCreationRequest("FirstName", "LastName", "CodeName", "UserName", "Email", "Password", "Avatar", Role.ASTRONAUT);
            var id = await UserService.Create(creationRequest);
            var user = await UserService.GetById(id);
            var newFirstName = "NewFirstName";
            await UserService.Update(new User(user.Id, newFirstName, user.LastName, user.CodeName, user.Username, user.Email, user.Avatar, user.Role));
            var expected = new User(id, newFirstName, creationRequest.LastName, creationRequest.CodeName, creationRequest.Username, creationRequest.Email, creationRequest.Avatar, creationRequest.Role);
            var actual = await UserService.GetById(id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task testUpdateThrowsWhenUsernameExists()
        {
            var existingUsername = "ExistingUsername";
            await UserService.Create(new UserCreationRequest("FirstName", "LastName", "CodeName", existingUsername, "Email", "Password", "Avatar", Role.ASTRONAUT));
            var creationRequest = new UserCreationRequest("FirstName", "LastName", "CodeName", "UserName", "Email", "Password", "Avatar", Role.ASTRONAUT);
            var id = await UserService.Create(creationRequest);
            await Assert.ThrowsExceptionAsync<UserAlreadyExistsException>(async () =>
            {
                await UserService.Update(new User(id, creationRequest.FirstName, creationRequest.LastName, creationRequest.CodeName, existingUsername, creationRequest.Email, creationRequest.Avatar, creationRequest.Role));
            });
        }

        [TestMethod]
        public async Task testChangePassword()
        {
            var creationRequest = new UserCreationRequest("FirstName", "LastName", "CodeName", "UserName", "Email", "Password", "Avatar", Role.ASTRONAUT);
            var newPassword = "NewPassword";
            var id = await UserService.Create(creationRequest);
            await UserService.ChangePassword(id, newPassword);
            var entity = await UserRepository.GetById(id);
            Assert.IsTrue(PasswordHelper.Check(entity.Password, newPassword));
        }
    }
}
