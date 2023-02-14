using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MissionReportingTool.Entities;
using MissionReportingTool.Entitites;
using MissionReportingTool.Helpers;

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

            modelBuilder
                .Entity<UserEntity>()
                .Property(d => d.Role)
                .HasConversion(new EnumToStringConverter<Role>());

            var id = 1;
            modelBuilder.Entity<UserEntity>()
                .HasData(
                    new UserEntity
                    {
                        Id = id++,
                        FirstName = "Yury",
                        LastName = "Gagarin",
                        CodeName = "First Man",
                        Username = "yuga",
                        Email = "yuga@mtr.moon",
                        Password = PasswordHelper.Hash("poleposition1"),
                        Avatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/ff/Yuri_Gagarin_in_1963.jpg/640px-Yuri_Gagarin_in_1963.jpg",
                        Role = Role.ASTRONAUT
                    },
                    new UserEntity
                    {
                        Id = id++,
                        FirstName = "Alan",
                        LastName = "Shepard",
                        CodeName = "Shepard",
                        Username = "alsh",
                        Email = "alsh@mtr.moon",
                        Password = PasswordHelper.Hash("secret"),
                        Avatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ae/Alan_Shepard_astronaut_in_spacesuit.jpg/640pxAlan_Shepard_astronaut_in_spacesuit.jpg",
                        Role = Role.ASTRONAUT
                    },
                    new UserEntity
                    {
                        Id = id++,
                        FirstName = "Valentina",
                        LastName = "Tereshkova",
                        CodeName = "Valentine",
                        Username = "vate",
                        Email = "vate@mtr.moon",
                        Password = PasswordHelper.Hash("DQ!cnRVYzQ64@Fwha!XB_kYn"),
                        Avatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/61/RIAN_archive_612748_Valentina_Tereshkova.jpg/640pxRIAN_archive_612748_Valentina_Tereshkova.jpg",
                        Role = Role.ASTRONAUT
                    },
                    new UserEntity
                    {
                        Id = id++,
                        FirstName = "Guion",
                        LastName = "Bluford",
                        CodeName = "Bluey",
                        Username = "gubl",
                        Email = "gubl@mtr.moon",
                        Password = PasswordHelper.Hash("STS-8!Challenger1983"),
                        Avatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/04/Guion_Bluford.jpg/640px-Guion_Bluford.jpg",
                        Role = Role.ASTRONAUT
                    },
                    new UserEntity
                    {
                        Id = id++,
                        FirstName = "Andreas",
                        LastName = "Mogensen",
                        CodeName = "Great Dane",
                        Username = "anmo",
                        Email = "anmo@mtr.moon",
                        Password = PasswordHelper.Hash("rødgrødmedfløde"),
                        Avatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/66/ISS-44_Andreas_Mogensen_in_the_Columbus_module.jpg/640pxISS-44_Andreas_Mogensen_in_the_Columbus_module.jpg",
                        Role = Role.ASTRONAUT
                    },
                    new UserEntity
                    {
                        Id = id++,
                        FirstName = "Yi",
                        LastName = "So-Yeon",
                        CodeName = "Neon",
                        Username = "yiso",
                        Email = "yiso@mtr.moon",
                        Password = PasswordHelper.Hash("K2t@dACRkGCd3-UQQmCZJbTj"),
                        Avatar = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b3/Yi_Soyeon_%28NASA_-_JSC2008-E-004174%29.jpg/640px-Yi_So-yeon_%28NASA_-_JSC2008-E-004174%29.jpg",
                        Role = Role.ASTRONAUT
                    },
                    new UserEntity
                    {
                        Id = id++,
                        FirstName = "Admin",
                        LastName = "Adminsen",
                        CodeName = "SuperUser",
                        Username = "admin",
                        Email = "admin@system.com",
                        Password = PasswordHelper.Hash("admin"),
                        Avatar = "https://thumbs.dreamstime.com/b/admin-sign-laptop-icon-stock-vector-166205404.jpg",
                        Role = Role.ADMIN
                    },
                    new UserEntity
                    {
                        Id = id++,
                        FirstName = "John",
                        LastName = "Doe",
                        CodeName = "ScienceGuy",
                        Username = "science",
                        Email = "science@science.com",
                        Password = PasswordHelper.Hash("science"),
                        Avatar = "https://img.freepik.com/free-vector/flat-illustration-biotechnology-concept_23-2148880770.jpg?w=2000",
                        Role = Role.SCIENTIEST
                    },
                    new UserEntity
                    {
                        Id = id++,
                        FirstName = "S.",
                        LastName = "Down",
                        CodeName = "Space General",
                        Username = "general",
                        Email = "general@space.com",
                        Password = PasswordHelper.Hash("general"),
                        Avatar = "https://www.shutterstock.com/image-illustration/cartoon-military-general-helmet-on-600w-62645065.jpg",
                        Role = Role.GENERAL
                    }
                );
        }
    }
}
