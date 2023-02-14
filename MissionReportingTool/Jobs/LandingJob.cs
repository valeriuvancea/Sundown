using MissionReportingTool.Entitites;
using MissionReportingTool.Exceptions;
using MissionReportingTool.Helpers;
using MissionReportingTool.Repositories.Interfaces;
using Quartz;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MissionReportingTool.Jobs
{
    public class LandingJob : IJob
    {
        private readonly string IssLocationApiEndpoint;
        private readonly HttpClient HttpClient;
        private readonly ILogger<LandingJob> Logger;
        private readonly ILandingRepository LandingRepository;

        public LandingJob(IConfiguration configuration, HttpClient httpClient, ILogger<LandingJob> logger, ILandingRepository landingRepository)
        {
            this.IssLocationApiEndpoint = configuration["IssLocationApiEndpoint"];
            this.HttpClient = httpClient;
            this.Logger = logger;
            this.LandingRepository = landingRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Logger.LogInformation("Obtaining ISS location");
            IssLocationResponse issLocation;
            try
            {
                var response = await HttpClient.GetStringAsync(IssLocationApiEndpoint + "?timestamps=" + ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds());
                issLocation = JsonSerializer.Deserialize<List<IssLocationResponse>>(response).FirstOrDefault();
            }
            catch (Exception exception)
            {
                Logger.LogError("Exception encountered during and HTTP call", exception);
                throw new HttpCallException(exception);
            }
            if (issLocation != null)
            {
                Logger.LogInformation("Current ISS location `{}`", issLocation);
                var closestFacility = DistanceHelper.CalculateTheClosestFacility(issLocation.latitude, issLocation.longitude);
                Logger.LogInformation("Closest facility to the ISS `{}`", closestFacility);
                await LandingRepository.Add(closestFacility);
            } else
            {
                Logger.LogError("Null ISS location");
            }
        }
    }
}
