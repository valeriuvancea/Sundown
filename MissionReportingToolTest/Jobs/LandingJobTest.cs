using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MissionReportingTool.Contracts.Responses;
using MissionReportingTool.Delegates.EventsArgs;
using MissionReportingTool.Jobs;
using MissionReportingTool.Repositories.Interfaces;
using MissionReportingTool.Services.Interfaces;
using MissionReportingTool.Services;
using MissionReportingToolTest.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using MissionReportingTool.Entitites;
using MissionReportingTool.Exceptions;

namespace MissionReportingToolTest.Jobs
{
    [TestClass]
    public class LandingJobTest
    {

        private static readonly ServiceProvider ServiceProvider = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();
        private static readonly ILoggerFactory LoggerFactory = ServiceProvider.GetService<ILoggerFactory>();
        private static readonly ILogger<LandingJob> Logger = LoggerFactory.CreateLogger<LandingJob>();
        private static readonly Dictionary<string, string> InMemorySettings = new Dictionary<string, string> {
            {"IssLocationApiEndpoint", "http://testIssLocation.com"},
        };
        private static readonly IConfiguration Configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(InMemorySettings)
            .Build();
        private static readonly ILandingRepository LandingRepository = new FakeLandingRepository();
        private static readonly IssLocationResponse IssLocationResponse = new IssLocationResponse(1, 0, 0);
        private static readonly string IssLocationResponseJson = $"[{JsonSerializer.Serialize(IssLocationResponse)}]";
        private static HttpClient HttpClient;
        private static LandingJob LandingJob;
        private static LandedEventArgs? LandedEventArgs = null;

        [TestInitialize]
        public void init()
        {
            HttpClient = HttpClientMockHelper.GetHttpClient(
            new List<HttpClientMock>() {
                new HttpClientMock(InMemorySettings["IssLocationApiEndpoint"], IssLocationResponseJson),

            });
            LandingJob = new LandingJob(Configuration, HttpClient, Logger, LandingRepository);
        }

        [TestCleanup]
        public void clean()
        {
            LandedEventArgs = null;
            (LandingRepository as FakeLandingRepository)?.Clean();
        }

        [TestMethod]
        public async Task testExecute()
        {
            await LandingJob.Execute(null);
            var actual = (await LandingRepository.GetLastClosestLandingPosition()).Facility;
            var expected = Facility.AFRICA;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task testExecuteThrowsWhenThereIsAnHttpExeptionFromTheIssApi()
        {
            await LandingJob.Execute(null);
            // The HTTP mock work only for one call, so the second call will throw an exception
            await Assert.ThrowsExceptionAsync<HttpCallException>(async () =>
            {
                await LandingJob.Execute(null);
            });
        }
    }
}
