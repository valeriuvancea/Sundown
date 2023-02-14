using MissionReportingTool.Contracts.Responses;
using MissionReportingTool.Delegates;
using MissionReportingTool.Delegates.EventsArgs;
using MissionReportingTool.Exceptions;
using MissionReportingTool.Handlers;
using MissionReportingTool.Jobs;
using MissionReportingTool.Repositories;
using MissionReportingTool.Repositories.Interfaces;
using MissionReportingTool.Services.Interfaces;
using System.Text.Json;

namespace MissionReportingTool.Services
{
    public class CommandsService : ICommandsService
    {
        private readonly string OpenMeteoApiEndpoint;
        private readonly ILandingRepository LandingRepository;
        private readonly HttpClient HttpClient;
        private event LandDelegate? LandPublisher;
        private readonly LandingJob LandingJob;
        private readonly ILogger<CommandsService> Logger;

        public CommandsService(ILandingRepository landingRepository, HttpClient httpClient, IConfiguration configuration, LandHandler landHandler, ILoggerFactory loggerFactory)
        {
            OpenMeteoApiEndpoint = configuration["OpenMeteoApiEndpoint"];
            LandingRepository = landingRepository;
            HttpClient = httpClient;
            LandPublisher += landHandler.Handle;
            LandingJob = new LandingJob(configuration, httpClient, loggerFactory.CreateLogger<LandingJob>(), landingRepository);
            Logger = loggerFactory.CreateLogger<CommandsService>();
        }
        public async Task Land()
        {
            var closestLanding = await LandingRepository.GetLastClosestLandingPosition();
            if (closestLanding == null)
            {
                await LandingJob.Execute(null);
                closestLanding = await LandingRepository.GetLastClosestLandingPosition();

            }
            var closestLandingFacility = closestLanding.Facility;
            OpenMeteoResponse openMeteoResponse;
            try
            {
                var result = await HttpClient.GetStringAsync(OpenMeteoApiEndpoint + $"?latitude={closestLandingFacility.Latitude}&longitude={closestLandingFacility.Longitude}&hourly=temperature_2m");
                openMeteoResponse = JsonSerializer.Deserialize<OpenMeteoResponse>(result);
            }
            catch (Exception exception)
            {
                Logger.LogError("Exception encountered during and HTTP call", exception);
                throw new HttpCallException(exception);
            }
            if (openMeteoResponse != null)
            {
                var minimumTemperatureIndex = openMeteoResponse.hourly.temperature_2m.Select((item, index) => (item, index)).Min().index;
                var landingTime = openMeteoResponse.hourly.time.ElementAt(minimumTemperatureIndex);
                LandPublisher?.Invoke(new LandedEventArgs { LandingTime = landingTime, Facility = closestLandingFacility });
                Logger.LogInformation("LandedEvent published with landingTime `{}`, and facility `{}`", landingTime, closestLandingFacility);
            }
        }
    }
}
