using Microsoft.EntityFrameworkCore.Migrations;

namespace DandyBox.Core.Migrations
{
    public partial class AddMovieSeries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Series",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Series",
                table: "Movies");
        }
    }
}
