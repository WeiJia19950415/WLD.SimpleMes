using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations
{
    public partial class WorkOrderBomItem_Alter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderBomItems_MaterialInfos_InputMaterialInfoId",
                table: "WorkOrderBomItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrderBomItems_InputMaterialInfoId",
                table: "WorkOrderBomItems");

            migrationBuilder.DropColumn(
                name: "InputMaterialInfoId",
                table: "WorkOrderBomItems");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderBomItems_InputMaterialId",
                table: "WorkOrderBomItems",
                column: "InputMaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderBomItems_MaterialInfos_InputMaterialId",
                table: "WorkOrderBomItems",
                column: "InputMaterialId",
                principalTable: "MaterialInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderBomItems_MaterialInfos_InputMaterialId",
                table: "WorkOrderBomItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrderBomItems_InputMaterialId",
                table: "WorkOrderBomItems");

            migrationBuilder.AddColumn<long>(
                name: "InputMaterialInfoId",
                table: "WorkOrderBomItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderBomItems_InputMaterialInfoId",
                table: "WorkOrderBomItems",
                column: "InputMaterialInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderBomItems_MaterialInfos_InputMaterialInfoId",
                table: "WorkOrderBomItems",
                column: "InputMaterialInfoId",
                principalTable: "MaterialInfos",
                principalColumn: "Id");
        }
    }
}
