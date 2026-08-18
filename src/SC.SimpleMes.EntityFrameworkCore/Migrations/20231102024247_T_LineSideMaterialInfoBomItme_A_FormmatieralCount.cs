using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations
{
    public partial class T_LineSideMaterialInfoBomItme_A_FormmatieralCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FormMaterialAmount",
                table: "LineSideMaterialInfoBomItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormMaterialAmount",
                table: "LineSideMaterialInfoBomItems");
        }
    }
}
