using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeagueTableApp.DAL.Migrations
{
    public partial class NagyhaziInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.Id);
                    table.UniqueConstraint("AK_Leagues_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Coach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Captain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Players = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.UniqueConstraint("AK_Teams_LeagueId_Name", x => new { x.LeagueId, x.Name });
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeagueId = table.Column<int>(type: "int", nullable: false),
                    HomeTeamId = table.Column<int>(type: "int", nullable: true),
                    ForeignTeamId = table.Column<int>(type: "int", nullable: true),
                    HomeTeamScore = table.Column<double>(type: "float", nullable: false),
                    ForeignTeamScore = table.Column<double>(type: "float", nullable: false),
                    IsEnded = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_ForeignTeamId",
                        column: x => x.ForeignTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Description", "IsDeleted", "Name" },
                values: new object[] { 101, "It's a league for testing, for example test changeing values or deleting.", false, "TestLeague1" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Description", "IsDeleted", "Name" },
                values: new object[] { 102, "It's a league for testing, for example test changeing values or deleting.", false, "TestLeague2" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Captain", "Coach", "IsDeleted", "LeagueId", "Name", "Players" },
                values: new object[,]
                {
                    { 101, "Player2", "Test", false, 101, "TestTeam1", "Player1, Player2, Player3" },
                    { 102, "A", "Guardiola", false, 101, "TestTeam2", "A, B, C, D" },
                    { 103, "Y", "Rosssi", false, 101, "TestTeam3", "X, Y, Z" },
                    { 104, "jatekos3", "BelaBacsi", false, 102, "TestTeamForDelete", "jatekos1, jatekos2, jatekos3" }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ForeignTeamId",
                table: "Matches",
                column: "ForeignTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeTeamId",
                table: "Matches",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_IsEnded",
                table: "Matches",
                column: "IsEnded");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_LeagueId",
                table: "Matches",
                column: "LeagueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Leagues");
        }
    }
}
