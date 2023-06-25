using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeagueTableApp.DAL.Migrations
{
    public partial class proba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 1004);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 1005);

            migrationBuilder.DeleteData(
                table: "Matches",
                keyColumn: "Id",
                keyValue: 1006);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "Id", "ForeignTeamId", "ForeignTeamScore", "HomeTeamId", "HomeTeamScore", "IsDeleted", "IsEnded", "LeagueId" },
                values: new object[,]
                {
                    { 1001, 102, 1.0, 101, 2.0, false, true, 101 },
                    { 1002, 101, 0.0, 102, 0.0, false, false, 101 },
                    { 1003, 101, 1.0, 103, 0.0, false, true, 101 },
                    { 1004, 103, 0.0, 101, 0.0, false, false, 101 },
                    { 1005, 103, 0.0, 102, 0.0, false, false, 101 },
                    { 1006, 102, 2.0, 103, 2.0, false, true, 101 }
                });
        }
    }
}
