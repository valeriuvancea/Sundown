using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MissionReportingTool.Contracts.Requests;
using MissionReportingTool.Contracts.Responses;
using MissionReportingTool.Delegates.EventsArgs;
using MissionReportingTool.Entitites;
using MissionReportingTool.Exceptions;
using MissionReportingTool.Handlers;
using MissionReportingTool.Jobs;
using MissionReportingTool.Repositories.Interfaces;
using MissionReportingTool.Services;
using MissionReportingTool.Services.Interfaces;
using MissionReportingToolTest.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MissionReportingToolTest.Services
{
    [TestClass]
    public class CommandsServiceTest
    {
        private class FakeLandHandler : LandHandler
        {
            public FakeLandHandler() : base(LoggerFactory.CreateLogger<LandHandler>())
            {
            }

            public override void Handle(LandedEventArgs eventArgs)
            {
                LandedEventArgs = eventArgs;
            }
        }

        private static readonly ServiceProvider ServiceProvider = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();
        private static readonly ILoggerFactory LoggerFactory = ServiceProvider.GetService<ILoggerFactory>();
        private static readonly Dictionary<string, string> InMemorySettings = new Dictionary<string, string> {
            {"OpenMeteoApiEndpoint", "http://testMeteo.com"},
            {"IssLocationApiEndpoint", "http://testIssLocation.com"},
        };
        private static readonly IConfiguration Configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(InMemorySettings)
            .Build();
        private static readonly ILandingRepository LandingRepository = new FakeLandingRepository();
        private static readonly OpenMeteoResponse OpenMeteoResponse = new OpenMeteoResponse(
            new OpenMeteoResponse.Hourly(
                new List<DateTime>() {
                    DateTime.UtcNow, DateTime.UtcNow.AddMinutes(1),
                    DateTime.UtcNow.AddMinutes(2)
                },
                new List<double>() { 12, 11, 13 }
                ));
        private static readonly string OpenMeteoResponseJson = JsonSerializer.Serialize(OpenMeteoResponse);
        private static readonly IssLocationResponse IssLocationResponse = new IssLocationResponse(1, 0, 0);
        private static readonly string IssLocationResponseJson = $"[{JsonSerializer.Serialize(IssLocationResponse)}]";
        private static HttpClient HttpClient;
        private static ICommandsService CommandsService;
        private static LandedEventArgs? LandedEventArgs = null;

        [TestInitialize]
        public void init()
        {
            HttpClient = HttpClientMockHelper.GetHttpClient(
            new List<HttpClientMock>() {
                new HttpClientMock(InMemorySettings["OpenMeteoApiEndpoint"], OpenMeteoResponseJson),
                new HttpClientMock(InMemorySettings["IssLocationApiEndpoint"], IssLocationResponseJson),

            });
            CommandsService = new CommandsService(LandingRepository, HttpClient, Configuration, new FakeLandHandler(), LoggerFactory);
        }

        [TestCleanup]
        public void clean()
        {
            LandedEventArgs = null;
            (LandingRepository as FakeLandingRepository)?.Clean();
        }

        [TestMethod]
        public async Task testLand()
        {
            var facility = Facility.EUROPE;
            await LandingRepository.Add(facility);
            await CommandsService.Land();
            Assert.AreEqual(OpenMeteoResponse.hourly.time[1], LandedEventArgs?.LandingTime);
            Assert.AreEqual(facility, LandedEventArgs?.Facility);
        }

        [TestMethod]
        public async Task testLandWhenThereIsNoFacilitySaved()
        {
            await CommandsService.Land();
            Assert.AreEqual(OpenMeteoResponse.hourly.time[1], LandedEventArgs?.LandingTime);
            Assert.AreEqual(Facility.AFRICA, LandedEventArgs?.Facility);
        }

        [TestMethod]
        public async Task testLandThrowsWhenThereIsAnHttpExeptionFromTheWeatherApi()
        {
            var facility = Facility.EUROPE;
            await LandingRepository.Add(facility);
            await CommandsService.Land();
            // The HTTP mock work only for one call, so the second call will throw an exception
            await Assert.ThrowsExceptionAsync<HttpCallException>(async () =>
            {
                await CommandsService.Land();
            });
        }

        [TestMethod]
        public async Task testLandThrowsWhenThereIsAnHttpExeptionFromTheIssApi()
        {
            await CommandsService.Land();
            // The HTTP mock work only for one call, so the second call will throw an exception
            await Assert.ThrowsExceptionAsync<HttpCallException>(async () =>
            {
                await CommandsService.Land();
            });
        }
    }
}
