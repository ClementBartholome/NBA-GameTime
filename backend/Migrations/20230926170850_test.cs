using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoxScores_Games_GameId",
                table: "BoxScores");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_HomeTeamId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Teams_VisitorTeamId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_HomeTeamId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_VisitorTeamId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "HomeTeamId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "HomeTeamScore",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Games",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "VisitorTeamScore",
                table: "Games",
                newName: "visitor_team_score");

            migrationBuilder.RenameColumn(
                name: "VisitorTeamId",
                table: "Games",
                newName: "home_team_score");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "BoxScores",
                newName: "Gameid");

            migrationBuilder.RenameIndex(
                name: "IX_BoxScores_GameId",
                table: "BoxScores",
                newName: "IX_BoxScores_Gameid");

            migrationBuilder.AddColumn<string>(
                name: "home_team_full_name",
                table: "Games",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "home_team_name",
                table: "Games",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "visitor_team_name",
                table: "Games",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "visitor_team_name_full_name",
                table: "Games",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_BoxScores_Games_Gameid",
                table: "BoxScores",
                column: "Gameid",
                principalTable: "Games",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoxScores_Games_Gameid",
                table: "BoxScores");

            migrationBuilder.DropColumn(
                name: "home_team_full_name",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "home_team_name",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "visitor_team_name",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "visitor_team_name_full_name",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Games",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "visitor_team_score",
                table: "Games",
                newName: "VisitorTeamScore");

            migrationBuilder.RenameColumn(
                name: "home_team_score",
                table: "Games",
                newName: "VisitorTeamId");

            migrationBuilder.RenameColumn(
                name: "Gameid",
                table: "BoxScores",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_BoxScores_Gameid",
                table: "BoxScores",
                newName: "IX_BoxScores_GameId");

            migrationBuilder.AddColumn<int>(
                name: "HomeTeamId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeTeamScore",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Games_HomeTeamId",
                table: "Games",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_VisitorTeamId",
                table: "Games",
                column: "VisitorTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoxScores_Games_GameId",
                table: "BoxScores",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_HomeTeamId",
                table: "Games",
                column: "HomeTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Teams_VisitorTeamId",
                table: "Games",
                column: "VisitorTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
