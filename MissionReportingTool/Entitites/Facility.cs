using System.Collections.Immutable;

namespace MissionReportingTool.Entitites
{
    public class Facility
    {
        public static readonly Facility EUROPE = new Facility("EUROPE", 55.68474022214539, 12.50971483525464);
        public static readonly Facility CHINA = new Facility("CHINA", 41.14962602664463, 119.33727554032843);
        public static readonly Facility AMERICA = new Facility("AMERICA", 40.014407426017335, -103.68329704730307);
        public static readonly Facility AFRICA = new Facility("AFRICA", -21.02973667221353, 23.77076788325546);
        public static readonly Facility AUSTRALIA = new Facility("AUSTRALIA", -33.00702098732439, 117.83314818861444);
        public static readonly Facility INDIA = new Facility("INDIA", 19.330540162912126, 79.14236662251713);
        public static readonly Facility ARGENTINA = new Facility("ARGENTINA", -34.050351176517886, -65.92682965568743);
        public static readonly IImmutableList<Facility> FACILITIES = ImmutableList.Create(
            EUROPE,
            CHINA,
            AMERICA,
            AFRICA,
            AUSTRALIA,
            INDIA,
            ARGENTINA);


        public string Name { get; }
        public double Latitude { get; }
        public double Longitude { get; }

        private Facility(string name, double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public static Facility fromName(string name)
        {
            return FACILITIES.First(f => f.Name == name);
        }
    }
}
