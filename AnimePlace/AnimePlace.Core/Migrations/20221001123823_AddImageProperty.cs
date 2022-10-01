using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimePlace.Core.Migrations
{
    public partial class AddImageProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Animes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Animes");
        }
    }
}
