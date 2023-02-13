using Microsoft.EntityFrameworkCore;

namespace MissionReportingTool.Model
{
    public class SundownContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<MissionReport> MissionReports { get; set; }
        public DbSet<MissionImage> MissionImage { get; set; }


    }
}
