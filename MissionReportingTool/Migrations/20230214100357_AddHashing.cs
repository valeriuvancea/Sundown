using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MissionReportingTool.Migrations
{
    /// <inheritdoc />
    public partial class AddHashing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "1000.HzNyhtnzMa7fgGJsGmuEkg==.lP60NqH2IyjM6QcCPtsIOcPQ8PY1e6aaoCPkPVRJ6/c=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Password",
                value: "1000.oe9VQGho1VaIIoKEn7G/XA==.XpphCNVk4KmZcg9apKnoGHhIl1uBXjLUHLcQbljH+9M=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Password",
                value: "1000.BUMxWM6Z4MiVxlYWSJOGTQ==.ENn4jM5Dp9ZdbL6CgikXUUqnFTVg1OhUJ5o8XSPBwvY=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Password",
                value: "1000.reJwC1RLKmFRXMFJPLtAKw==.QChVF7+ZSveGpkcHpWv8DfbKgREuK2lmigwdsa0uB6k=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Password",
                value: "1000.k+ygpGe7K4U53oDyvkdwzA==.Nn8kiN1iRWDBt2vBjEkdDWhQbOCVyYazxH8V3y5p5is=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Password",
                value: "1000.taSKdgfZ9Bxt+ie1PZzd9w==.ZM/rLlYzi9O6o1GPZ99XuDAHLISs7wAe764TMqN3T98=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Password",
                value: "1000.AE00gX35YIMYkP6R9ZxBRQ==.HX8sXson15lIqi9aOCWQixkE8ucOJEBcpn55fLjGkSA=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Password",
                value: "1000.Yt4aGG8O8E3RGCh66z1fhQ==.s3IPPAJ6HA4bsSLEYltXdm40OKLAk4Q/X8x+/EEgzpc=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Password",
                value: "1000.j7dB/eQQkQ1WMhOhFBSlAg==.DUd/OnRi+RLbf9kq4Y2/RjuTnDSGckzEDXN/5876AOc=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "poleposition1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Password",
                value: "secret");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Password",
                value: "DQ!cnRVYzQ64@Fwha!XB_kYn");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Password",
                value: "STS-8!Challenger1983");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Password",
                value: "rødgrødmedfløde");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Password",
                value: "K2t@dACRkGCd3-UQQmCZJbTj");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Password",
                value: "admin");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Password",
                value: "science");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Password",
                value: "general");
        }
    }
}
