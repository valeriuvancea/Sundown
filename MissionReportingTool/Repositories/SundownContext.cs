using Microsoft.EntityFrameworkCore;
using MissionReportingTool.Entities;

namespace MissionReportingTool.Repositories
{
    public class SundownContext : DbContext
    {
        private readonly IConfiguration Configuration;
        public DbSet<User> Users { get; set; }
        public DbSet<MissionReport> MissionReports { get; set; }
        public DbSet<MissionImage> MissionImage { get; set; }

        public SundownContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration["DbConnectionString"]);
        }
    }
}
