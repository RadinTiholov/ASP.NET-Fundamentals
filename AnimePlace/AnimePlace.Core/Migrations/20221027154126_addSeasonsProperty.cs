using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimePlace.Core.Migrations
{
    public partial class addSeasonsProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Seasons",
                table: "Animes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seasons",
                table: "Animes");
        }
    }
}
