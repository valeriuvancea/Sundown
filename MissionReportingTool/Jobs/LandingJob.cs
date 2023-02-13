using MissionReportingTool.Entitites;
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

        public Facility CalculateTheClosestFacility(double latitude, double longitude)
        {
            return Facility.FACILITIES.MinBy(f =>
                Math.Abs(DistanceHelper.GetDistance(f.Latitude, f.Longitude, latitude, longitude))
            );
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Logger.LogInformation("Obtaining ISS location");
            var response = await HttpClient.GetStringAsync(IssLocationApiEndpoint + "?timestamps=" + ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds());
            var issLocation = JsonSerializer.Deserialize<List<IssLocationResponse>>(response).FirstOrDefault();
            if (issLocation != null)
            {
                Logger.LogInformation("Current ISS location `{}`", issLocation);
                var closestFacility = CalculateTheClosestFacility(issLocation.latitude, issLocation.longitude);
                Logger.LogInformation("Closest facility to the ISS `{}`", closestFacility);
                await LandingRepository.Add(closestFacility);
            } else
            {
                Logger.LogError("Null ISS location");
            }
        }
    }
}
