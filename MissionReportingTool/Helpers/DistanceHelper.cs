using MissionReportingTool.Entitites;

namespace MissionReportingTool.Helpers
{
    public class DistanceHelper
    {
        private DistanceHelper() { }

        /// <summary>
        /// Inspired from https://stackoverflow.com/a/24712129
        /// </summary>
        public static double GetDistance(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            double rLatitude1 = Math.PI * latitude1 / 180;
            double rLatitude2 = Math.PI * latitude2 / 180;
            double theta = longitude1 - longitude2;
            double rTheta = Math.PI * theta / 180;
            double distance =
                Math.Sin(rLatitude1) * Math.Sin(rLatitude2) + Math.Cos(rLatitude1) *
                Math.Cos(rLatitude2) * Math.Cos(rTheta);
            distance = Math.Acos(distance);
            distance = distance * 180 / Math.PI;
            distance = distance * 60 * 1.1515;

            return distance * 1.609344;
        }

        public static Facility CalculateTheClosestFacility(double latitude, double longitude)
        {
            return Facility.FACILITIES.MinBy(f =>
                Math.Abs(GetDistance(f.Latitude, f.Longitude, latitude, longitude))
            );
        }
    }
}
