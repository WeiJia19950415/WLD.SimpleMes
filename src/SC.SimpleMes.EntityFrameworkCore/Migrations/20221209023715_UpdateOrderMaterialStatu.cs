using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations
{
    public partial class UpdateOrderMaterialStatu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderMaterialProduceStatuses_WorkProcessInfos_WorkProcessInfoId",
                table: "OrderMaterialProduceStatuses");

            migrationBuilder.DropIndex(
                name: "IX_OrderMaterialProduceStatuses_WorkProcessInfoId",
                table: "OrderMaterialProduceStatuses");

            migrationBuilder.DropColumn(
                name: "WorkProcessInfoId",
                table: "OrderMaterialProduceStatuses");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMaterialProduceStatuses_CurrentWorkProcessId",
                table: "OrderMaterialProduceStatuses",
                column: "CurrentWorkProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderMaterialProduceStatuses_WorkProcessInfos_CurrentWorkProcessId",
                table: "OrderMaterialProduceStatuses",
                column: "CurrentWorkProcessId",
                principalTable: "WorkProcessInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderMaterialProduceStatuses_WorkProcessInfos_CurrentWorkProcessId",
                table: "OrderMaterialProduceStatuses");

            migrationBuilder.DropIndex(
                name: "IX_OrderMaterialProduceStatuses_CurrentWorkProcessId",
                table: "OrderMaterialProduceStatuses");

            migrationBuilder.AddColumn<long>(
                name: "WorkProcessInfoId",
                table: "OrderMaterialProduceStatuses",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderMaterialProduceStatuses_WorkProcessInfoId",
                table: "OrderMaterialProduceStatuses",
                column: "WorkProcessInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderMaterialProduceStatuses_WorkProcessInfos_WorkProcessInfoId",
                table: "OrderMaterialProduceStatuses",
                column: "WorkProcessInfoId",
                principalTable: "WorkProcessInfos",
                principalColumn: "Id");
        }
    }
}
