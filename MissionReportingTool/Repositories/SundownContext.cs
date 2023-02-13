using Microsoft.EntityFrameworkCore;
using MissionReportingTool.Entities;
using MissionReportingTool.Entitites;

namespace MissionReportingTool.Repositories
{
    public class SundownContext : DbContext
    {
        private readonly string DbConnectionString;
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<MissionReportEntity> MissionReports { get; set; }
        public DbSet<MissionImageEntity> MissionImage { get; set; }
        public DbSet<LandingEntity> Landings { get; set; }

        public SundownContext(IConfiguration configuration)
        {
            DbConnectionString = configuration["DbConnectionString"];
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(DbConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<LandingEntity>()
                .Property(e => e.Facility)
                .HasConversion(
                    v => v.ToString(),
                    v => Facility.fromName(v)
                );

            modelBuilder.Entity<UserEntity>()
                .HasData(
                    new UserEntity
                    {
                        Id = 1,
                        FirstName = "Yury",
                        LastName = "Gagarin",
                        CodeName = "First Man",
                        Username = "yuga",
                        Email = "yuga@mtr.moon",
                        Password = "poleposition1",
                        Avatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/ff/Yuri_Gagarin_in_1963.jpg/640px-Yuri_Gagarin_in_1963.jpg"
                    },
                    new UserEntity
                    {
                        Id = 2,
                        FirstName = "Alan",
                        LastName = "Shepard",
                        CodeName = "Shepard",
                        Username = "alsh",
                        Email = "alsh@mtr.moon",
                        Password = "secret",
                        Avatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ae/Alan_Shepard_astronaut_in_spacesuit.jpg/640pxAlan_Shepard_astronaut_in_spacesuit.jpg",
                    },
                    new UserEntity
                    {
                        Id = 3,
                        FirstName = "Valentina",
                        LastName = "Tereshkova",
                        CodeName = "Valentine",
                        Username = "vate",
                        Email = "vate@mtr.moon",
                        Password = "DQ!cnRVYzQ64@Fwha!XB_kYn",
                        Avatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/61/RIAN_archive_612748_Valentina_Tereshkova.jpg/640pxRIAN_archive_612748_Valentina_Tereshkova.jpg",
                    },
                    new UserEntity
                    {
                        Id = 4,
                        FirstName = "Guion",
                        LastName = "Bluford",
                        CodeName = "Bluey",
                        Username = "gubl",
                        Email = "gubl@mtr.moon",
                        Password = "STS-8!Challenger1983",
                        Avatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/04/Guion_Bluford.jpg/640px-Guion_Bluford.jpg",
                    },
                    new UserEntity
                    {
                        Id = 5,
                        FirstName = "Andreas",
                        LastName = "Mogensen",
                        CodeName = "Great Dane",
                        Username = "anmo",
                        Email = "anmo@mtr.moon",
                        Password = "rødgrødmedfløde",
                        Avatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/66/ISS-44_Andreas_Mogensen_in_the_Columbus_module.jpg/640pxISS-44_Andreas_Mogensen_in_the_Columbus_module.jpg",
                    },
                    new UserEntity
                    {
                        Id = 6,
                        FirstName = "Yi",
                        LastName = "So-Yeon",
                        CodeName = "Neon",
                        Username = "yiso",
                        Email = "yiso@mtr.moon",
                        Password = "K2t@dACRkGCd3-UQQmCZJbTj",
                        Avatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b3/Yi_Soyeon_%28NASA_-_JSC2008-E-004174%29.jpg/640px-Yi_So-yeon_%28NASA_-_JSC2008-E-004174%29.jpg",
                    }
                );
        }
    }
}
