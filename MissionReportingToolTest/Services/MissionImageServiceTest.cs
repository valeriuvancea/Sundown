using MissionReportingTool.Repositories.Interfaces;
using MissionReportingTool.Services.Interfaces;
using MissionReportingTool.Services;
using MissionReportingToolTest.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts;
using MissionReportingTool.Entitites;
using MissionReportingTool.Exceptions;

namespace MissionReportingToolTest.Services
{
    [TestClass]
    public class MissionImageServiceTest
    {
        private static readonly IUserRepository UserRepository = new FakeUserRepository();
        private static readonly UserService UserService = new UserService(UserRepository);
        private static readonly IMissionReportRepository MissionReportRepository = new FakeMissionReportRepository();
        private static readonly IMissionReportService MissionReportService = new MissionReportService(MissionReportRepository, UserService);
        private static readonly IMissionImageRepository MissionImageRepository = new FakeMissionImageRepository();
        private static readonly IMissionImageService MissionImageService = new MissionImageService(MissionImageRepository, MissionReportService);

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
            foreach (var x in await MissionImageService.GetByPaginationRequest(new PaginationRequest(0, 100)))
            {
                await MissionImageService.Delete(x.Id);
            }
        }

        [TestMethod]
        public async Task testCreate()
        {
            var userCreationRequest = new UserCreationRequest("FirstName", "LastName", "CodeName", "UserName", "Email", "Password", "Avatar", Role.ASTRONAUT);
            var userId = await UserService.Create(userCreationRequest);
            var missionReporCreationReuqest = new MissionReportCreationRequest("Name", "Description", 0, 0, DateTime.Now, DateTime.Now, userId);
            var missionReporId = await MissionReportService.Create(missionReporCreationReuqest);
            var missionImageCreationRequest = new MissionImageCreationRequest("CameraName", "RoverName", "RoverStatus", "Image", missionReporId);
            var missionImageId = await MissionImageService.Create(missionImageCreationRequest);
            var actual = await MissionImageService.GetById(missionImageId);
            var expected = new MissionImage(missionImageId, missionImageCreationRequest.CameraName, missionImageCreationRequest.RoverName, missionImageCreationRequest.RoverStatus, missionImageCreationRequest.Image, missionImageCreationRequest.MissionReportId);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task testCreateWithNonExistingMissionImageThrows()
        {
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () =>
            {
                await MissionImageService.Create(new MissionImageCreationRequest("CameraName", "RoverName", "RoverStatus", "Image", -1));
            });
        }

        [TestMethod]
        public async Task testUpdate()
        {
            var userCreationRequest = new UserCreationRequest("FirstName", "LastName", "CodeName", "UserName", "Email", "Password", "Avatar", Role.ASTRONAUT);
            var userId = await UserService.Create(userCreationRequest);
            var missionReporCreationReuqest = new MissionReportCreationRequest("Name", "Description", 0, 0, DateTime.Now, DateTime.Now, userId);
            var missionReporId = await MissionReportService.Create(missionReporCreationReuqest);
            var missionImageCreationRequest = new MissionImageCreationRequest("CameraName", "RoverName", "RoverStatus", "Image", missionReporId);
            var missionImageId = await MissionImageService.Create(missionImageCreationRequest);
            var newCameraName = "NewCameraName";
            var expected = new MissionImage(missionImageId, newCameraName, missionImageCreationRequest.RoverName, missionImageCreationRequest.RoverStatus, missionImageCreationRequest.Image, missionImageCreationRequest.MissionReportId);
            await MissionImageService.Update(expected);
            var actual = await MissionImageService.GetById(missionImageId);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task testUpdateNonExistingMissionImageThrows()
        {
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () =>
            {
                await MissionImageService.Update(new MissionImage(1, "CameraName", "RoverName", "RoverStatus", "Image", -1));
            });
        }

        [TestMethod]
        public async Task testUpdateWithNonMissionReportThrows()
        {
            var userCreationRequest = new UserCreationRequest("FirstName", "LastName", "CodeName", "UserName", "Email", "Password", "Avatar", Role.ASTRONAUT);
            var userId = await UserService.Create(userCreationRequest);
            var missionReporCreationReuqest = new MissionReportCreationRequest("Name", "Description", 0, 0, DateTime.Now, DateTime.Now, userId);
            var missionReporId = await MissionReportService.Create(missionReporCreationReuqest);
            var missionImageCreationRequest = new MissionImageCreationRequest("CameraName", "RoverName", "RoverStatus", "Image", missionReporId);
            var missionImageId = await MissionImageService.Create(missionImageCreationRequest);
            var newCameraName = "NewCameraName";
            var updatedMissionImage = new MissionImage(missionImageId, newCameraName, missionImageCreationRequest.RoverName, missionImageCreationRequest.RoverStatus, missionImageCreationRequest.Image, -1);
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () =>
            {
                await MissionImageService.Update(updatedMissionImage);
            });
        }
    }
}
