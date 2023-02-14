using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts;
using MissionReportingTool.Entitites;
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
using MissionReportingTool.Exceptions;

namespace MissionReportingToolTest.Services
{
    [TestClass]
    public class MissionReportServiceTest
    {
        private static readonly IUserRepository UserRepository = new FakeUserRepository();
        private static readonly UserService UserService = new UserService(UserRepository);
        private static readonly IMissionReportRepository MissionReportRepository = new FakeMissionReportRepository();
        private static readonly IMissionReportService MissionReportService = new MissionReportService(MissionReportRepository, UserService);

        [TestCleanup]
        public async Task clean()
        {
            foreach (var x in await UserService.GetByPaginationRequest(new PaginationRequest(0, 100)))
            {
                await UserService.Delete(x.Id);
            }
            foreach (var x in await MissionReportService.GetByPaginationRequest(new PaginationRequest(0, 100)))
            {
                await MissionReportService.Delete(x.Id);
            }
        }

        [TestMethod]
        public async Task testCreate()
        {
            var userCreationRequest = new UserCreationRequest("FirstName", "LastName", "CodeName", "UserName", "Email", "Password", "Avatar", Role.ASTRONAUT);
            var userId = await UserService.Create(userCreationRequest);
            var missionReporCreationReuqest = new MissionReportCreationRequest("Name", "Description", 0, 0, DateTime.Now, DateTime.Now, userId);
            var missionReporId = await MissionReportService.Create(missionReporCreationReuqest);
            var actual = await MissionReportService.GetById(missionReporId);
            var expected = new MissionReport(missionReporId, missionReporCreationReuqest.Name, missionReporCreationReuqest.Description, missionReporCreationReuqest.Latitude, missionReporCreationReuqest.Longitude, missionReporCreationReuqest.MissionDate, missionReporCreationReuqest.FinalisationDate, missionReporCreationReuqest.UserId);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task testCreateWithNonExistingUserThrows()
        {
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () =>
            {
                await MissionReportService.Create(new MissionReportCreationRequest("Name", "Description", 0, 0, DateTime.Now, DateTime.Now, -1));
            });
        }

        [TestMethod]
        public async Task testUpdate()
        {
            var userCreationRequest = new UserCreationRequest("FirstName", "LastName", "CodeName", "UserName", "Email", "Password", "Avatar", Role.ASTRONAUT);
            var userId = await UserService.Create(userCreationRequest);
            var missionReporCreationReuqest = new MissionReportCreationRequest("Name", "Description", 0, 0, DateTime.Now, DateTime.Now, userId);
            var missionReporId = await MissionReportService.Create(missionReporCreationReuqest);
            var newName = "NewName";
            var expected = new MissionReport(missionReporId, newName, missionReporCreationReuqest.Description, missionReporCreationReuqest.Latitude, missionReporCreationReuqest.Longitude, missionReporCreationReuqest.MissionDate, missionReporCreationReuqest.FinalisationDate, missionReporCreationReuqest.UserId);
            await MissionReportService.Update(expected);
            var actual = await MissionReportService.GetById(missionReporId);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task testUpdateNonExistingMissionReportThrows()
        {
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () =>
            {
                await MissionReportService.Update(new MissionReport(1,"Name", "Description", 0, 0, DateTime.Now, DateTime.Now, -1));
            });
        }

        [TestMethod]
        public async Task testUpdateWithNonExistingUserThrows()
        {
            var userCreationRequest = new UserCreationRequest("FirstName", "LastName", "CodeName", "UserName", "Email", "Password", "Avatar", Role.ASTRONAUT);
            var userId = await UserService.Create(userCreationRequest);
            var missionReporCreationReuqest = new MissionReportCreationRequest("Name", "Description", 0, 0, DateTime.Now, DateTime.Now, userId);
            var missionReporId = await MissionReportService.Create(missionReporCreationReuqest);
            var newName = "NewName";
            var updatedMissionReport = new MissionReport(missionReporId, newName, missionReporCreationReuqest.Description, missionReporCreationReuqest.Latitude, missionReporCreationReuqest.Longitude, missionReporCreationReuqest.MissionDate, missionReporCreationReuqest.FinalisationDate, -1);
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () =>
            {
                await MissionReportService.Update(updatedMissionReport);
            });
        }
    }
}
