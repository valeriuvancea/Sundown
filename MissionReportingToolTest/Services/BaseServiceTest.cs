using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Exceptions;
using MissionReportingTool.Repositories;
using MissionReportingToolTest.Mocks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionReportingToolTest.Services
{
    [TestClass]
    public class BaseServiceTest
    {
        private static readonly FakeRepository Repository = new FakeRepository();
        private static readonly FakeService Service = new FakeService(Repository);

        [TestCleanup]
        public async Task clean()
        {
            foreach (var x in await Service.GetByPaginationRequest(new PaginationRequest(0, 100)))
            {
                await Service.Delete(x.Id);
            }
        }

        [TestMethod]
        public async Task testCreate()
        {
            var creationRequest = new FakeCreationRequest("Name");
            var id = await Service.Create(creationRequest);
            var actual = await Service.GetById(id);
            var expected = new FakeContract(id, creationRequest.Name);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task testDelete()
        {
            var creationRequest = new FakeCreationRequest("Name");
            var id = await Service.Create(creationRequest);
            var actual = await Service.GetById(id);
            var expected = new FakeContract(id, creationRequest.Name);
            Assert.AreEqual(expected, actual);
            await Service.Delete(id);
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () =>
            {
                await Service.GetById(id);
            });
        }

        [TestMethod]
        public async Task testDeleteNonExisitngEntityThrows()
        {
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () =>
            {
                await Service.GetById(1);
            });
        }

        [TestMethod]
        public async Task testUpdate()
        {
            var creationRequest = new FakeCreationRequest("Name");
            var id = await Service.Create(creationRequest);
            var newName = "NewName";
            var expected = new FakeContract(id, newName);
            await Service.Update(expected);
            var actual = await Service.GetById(id);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task testUpdateNonExisitngEntityThrows()
        {
            await Assert.ThrowsExceptionAsync<EntityNotFoundException>(async () =>
            {
                await Service.Update(new FakeContract(1, "NonExisting"));
            });
        }


        [TestMethod]
        public async Task testGetByPaginationRequest()
        {
            var name1 = "Name1";
            var name2 = "Name2";
            var name3 = "Name3";
            var name4 = "Name4";
            var id1 = await Service.Create(new FakeCreationRequest(name1));
            var id2 = await Service.Create(new FakeCreationRequest(name2));
            var id3 = await Service.Create(new FakeCreationRequest(name3));
            var id4 = await Service.Create(new FakeCreationRequest(name4));

            var expected = new List<FakeContract>()
            {
                new FakeContract(id2, name2),
                new FakeContract(id3, name3),
            };
            var actual = await Service.GetByPaginationRequest(new PaginationRequest(id1, 2));
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
