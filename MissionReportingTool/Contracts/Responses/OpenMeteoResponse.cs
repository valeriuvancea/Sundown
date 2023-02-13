namespace MissionReportingTool.Contracts.Responses
{
    public record OpenMeteoResponse(OpenMeteoResponse.Hourly hourly)
    {
        public record Hourly(List<DateTime> time, List<double> temperature_2m)
        {

        }
    }
}
