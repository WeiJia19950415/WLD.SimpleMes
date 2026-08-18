using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations
{
    public partial class AddLineMaterilInfoBOM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaterialNumber",
                table: "LineSideMaterialInfos",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LineSideMaterialInfoBomItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineSideMaterialInfoId = table.Column<long>(type: "bigint", nullable: false),
                    FormMaterialCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    FormMaterialCategoryNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FormMaterialCategoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineSideMaterialInfoBomItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineSideMaterialInfoBomItems_LineSideMaterialInfos_LineSideMaterialInfoId",
                        column: x => x.LineSideMaterialInfoId,
                        principalTable: "LineSideMaterialInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineSideMaterialInfoBomItems_LineSideMaterialInfoId",
                table: "LineSideMaterialInfoBomItems",
                column: "LineSideMaterialInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineSideMaterialInfoBomItems");

            migrationBuilder.DropColumn(
                name: "MaterialNumber",
                table: "LineSideMaterialInfos");
        }
    }
}
