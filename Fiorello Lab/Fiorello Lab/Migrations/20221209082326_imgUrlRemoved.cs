using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiorello_Lab.Migrations
{
    public partial class imgUrlRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Flowers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Flowers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
