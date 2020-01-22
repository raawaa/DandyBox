using Microsoft.EntityFrameworkCore.Migrations;

namespace DandyBox.Core.Migrations
{
    public partial class AddMediaFileLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Length",
                table: "MediaFiles",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "MediaFiles");
        }
    }
}
