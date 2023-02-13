namespace MissionReportingTool.Helpers
{
    public class DistanceHelper
    {
        private DistanceHelper() { }

        public static double GetDistance(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            return Math.Acos(
                Math.Sin(latitude1) * Math.Sin(latitude2) +
                Math.Cos(latitude1) * Math.Cos(latitude2) * Math.Sin(longitude2 - longitude1))
                * 6371;
        }
    }
}
