using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MissionReportingTool.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    CodeName = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MissionReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    MissionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinalisationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionReports", x => x.Id);
                    table.ForeignKey("FK_User", x => x.UserId, "Users");
                });

            migrationBuilder.CreateTable(
                name: "MissionImage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CameraName = table.Column<string>(type: "text", nullable: false),
                    RoverName = table.Column<string>(type: "text", nullable: false),
                    RoverStatus = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    MissionReportId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionImage", x => x.Id);
                    table.ForeignKey("FK_MissionReport", x => x.MissionReportId, "MissionReports");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Avatar", "CodeName", "CreatedAt", "DeletedAt", "Email", "FirstName", "LastName", "Password", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { 1L, "https://upload.wikimedia.org/wikipedia/commons/thumb/f/ff/Yuri_Gagarin_in_1963.jpg/640px-Yuri_Gagarin_in_1963.jpg", "FirstName", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "yuga@mtr.moon", "Yury", "Gagarin", "poleposition1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yuga" },
                    { 2L, "https =//upload.wikimedia.org/wikipedia/commons/thumb/a/ae/Alan_Shepard_astronaut_in_spacesuit.jpg/640pxAlan_Shepard_astronaut_in_spacesuit.jpg", "Shepard", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "alsh@mtr.moon", "Alan", "Shepard", "secret", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "alsh" },
                    { 3L, "https =/upload.wikimedia.org/wikipedia/commons/thumb/6/61/RIAN_archive_612748_Valentina_Tereshkova.jpg/640pxRIAN_archive_612748_Valentina_Tereshkova.jpg", "Valentine", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "vate@mtr.moon", "Valentina", "Tereshkova", "DQ!cnRVYzQ64@Fwha!XB_kYn", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "vate" },
                    { 4L, "https =//upload.wikimedia.org/wikipedia/commons/thumb/0/04/Guion_Bluford.jpg/640px-Guion_Bluford.jpg", "Bluey", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "gubl@mtr.moon", "Guion", "Bluford", "STS-8!Challenger1983", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "gubl" },
                    { 5L, "https =//upload.wikimedia.org/wikipedia/commons/thumb/6/66/ISS-44_Andreas_Mogensen_in_the_Columbus_module.jpg/640pxISS-44_Andreas_Mogensen_in_the_Columbus_module.jpg", "Great Dane", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "anmo@mtr.moon", "Andreas", "Mogensen", "rødgrødmedfløde", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "anmo" },
                    { 6L, "https =//upload.wikimedia.org/wikipedia/commons/thumb/b/b3/Yi_Soyeon_%28NASA_-_JSC2008-E-004174%29.jpg/640px-Yi_So-yeon_%28NASA_-_JSC2008-E-004174%29.jpg", "Neon", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "yiso@mtr.moon", "Yi", "So-Yeon", "K2t@dACRkGCd3-UQQmCZJbTj", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "yiso" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissionImage");

            migrationBuilder.DropTable(
                name: "MissionReports");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
