using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class test3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Teams",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "visitor_team_name_full_name",
                table: "Games",
                newName: "visitor_team_full_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Teams",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "visitor_team_full_name",
                table: "Games",
                newName: "visitor_team_name_full_name");
        }
    }
}
