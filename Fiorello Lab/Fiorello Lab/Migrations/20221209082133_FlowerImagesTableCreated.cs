using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiorello_Lab.Migrations
{
    public partial class FlowerImagesTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlowerImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowerId = table.Column<int>(type: "int", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlowerImages_Flowers_FlowerId",
                        column: x => x.FlowerId,
                        principalTable: "Flowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowerImages_FlowerId",
                table: "FlowerImages",
                column: "FlowerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlowerImages");
        }
    }
}
