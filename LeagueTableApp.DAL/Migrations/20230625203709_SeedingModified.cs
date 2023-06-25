using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeagueTableApp.DAL.Migrations
{
    public partial class SeedingModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Captain", "Coach", "IsDeleted", "LeagueId", "Name", "Players" },
                values: new object[] { 105, "Aladár", "Csercsaszov", false, 102, "TestTeamForUpdate", "Aladár, Béla, Csanád, Dániel" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 105);
        }
    }
}
