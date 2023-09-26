using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerStats");

            migrationBuilder.DropTable(
                name: "BoxScores");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Teams",
                newName: "teamName");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Teams",
                newName: "primaryColor");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Standings",
                newName: "id");

            migrationBuilder.AddColumn<string>(
                name: "logo",
                table: "Teams",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "logo",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "teamName",
                table: "Teams",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "primaryColor",
                table: "Teams",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Standings",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "BoxScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Gameid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoxScores_Games_Gameid",
                        column: x => x.Gameid,
                        principalTable: "Games",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PlayerStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    BoxScoreId = table.Column<int>(type: "int", nullable: true),
                    HomeOrAway = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Minutes = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Rebounds = table.Column<int>(type: "int", nullable: false),
                    Started = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerStats_BoxScores_BoxScoreId",
                        column: x => x.BoxScoreId,
                        principalTable: "BoxScores",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BoxScores_Gameid",
                table: "BoxScores",
                column: "Gameid");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStats_BoxScoreId",
                table: "PlayerStats",
                column: "BoxScoreId");
        }
    }
}
