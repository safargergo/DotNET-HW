using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeagueTableApp.DAL.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeTeamId = table.Column<int>(type: "int", nullable: false),
                    ForeignTeamId = table.Column<int>(type: "int", nullable: false),
                    HomeTeamScore = table.Column<double>(type: "float", nullable: false),
                    ForeignTeamScore = table.Column<double>(type: "float", nullable: false),
                    IsEnded = table.Column<bool>(type: "bit", nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Orders_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categories_Products_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LeagueTeam",
                columns: table => new
                {
                    LeaguesId = table.Column<int>(type: "int", nullable: false),
                    TeamsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueTeam", x => new { x.LeaguesId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_LeagueTeam_Orders_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeagueTeam_Products_LeaguesId",
                        column: x => x.LeaguesId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LeagueId",
                table: "Categories",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TeamId",
                table: "Categories",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueTeam_TeamsId",
                table: "LeagueTeam",
                column: "TeamsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "LeagueTeam");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
