using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeagueTableApp.DAL.Migrations
{
    public partial class matchbenLegueIdAtnevezve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Leagues_MatchsLeagueId",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "MatchsLeagueId",
                table: "Matches",
                newName: "LeagueId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_MatchsLeagueId",
                table: "Matches",
                newName: "IX_Matches_LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Leagues_LeagueId",
                table: "Matches",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Leagues_LeagueId",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "LeagueId",
                table: "Matches",
                newName: "MatchsLeagueId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_LeagueId",
                table: "Matches",
                newName: "IX_Matches_MatchsLeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Leagues_MatchsLeagueId",
                table: "Matches",
                column: "MatchsLeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
