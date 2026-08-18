using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations
{
    public partial class Add_LastUpdateTime_WorkOrderMaterialStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateTime",
                table: "OrderMaterialProduceStatuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderMaterialProduceStatuses_CurrentProductLineId",
                table: "OrderMaterialProduceStatuses",
                column: "CurrentProductLineId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMaterialProduceStatuses_CurrentWorkStationId",
                table: "OrderMaterialProduceStatuses",
                column: "CurrentWorkStationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMaterialProduceStatuses_LastUpdateTime",
                table: "OrderMaterialProduceStatuses",
                column: "LastUpdateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderMaterialProduceStatuses_CurrentProductLineId",
                table: "OrderMaterialProduceStatuses");

            migrationBuilder.DropIndex(
                name: "IX_OrderMaterialProduceStatuses_CurrentWorkStationId",
                table: "OrderMaterialProduceStatuses");

            migrationBuilder.DropIndex(
                name: "IX_OrderMaterialProduceStatuses_LastUpdateTime",
                table: "OrderMaterialProduceStatuses");

            migrationBuilder.DropColumn(
                name: "LastUpdateTime",
                table: "OrderMaterialProduceStatuses");
        }
    }
}
