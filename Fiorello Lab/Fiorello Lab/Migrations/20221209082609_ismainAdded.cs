using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiorello_Lab.Migrations
{
    public partial class ismainAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "FlowerImages",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "FlowerImages");
        }
    }
}
