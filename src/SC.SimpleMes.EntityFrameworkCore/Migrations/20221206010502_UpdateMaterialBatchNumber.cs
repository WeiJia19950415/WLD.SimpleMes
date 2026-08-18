using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations
{
    public partial class UpdateMaterialBatchNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_OrderMaterialProduceStatuses_MaterialInfos_MaterialInfoId",
            //    table: "OrderMaterialProduceStatuses");

            migrationBuilder.DropIndex(
                name: "IX_OrderMaterialProduceStatuses_MaterialInfoId",
                table: "OrderMaterialProduceStatuses");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "OrderMaterialProduceStatuses");

            migrationBuilder.AlterColumn<long>(
                name: "WorkOrderBomId",
                table: "WorkOrderInfos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryTime",
                table: "WorkOrderInfos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<long>(
                name: "WorkProcessSetId",
                table: "WorkOrderInfos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TargetNotifiers",
                table: "UserNotifications",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "MaterialInfoId",
                table: "OrderMaterialProduceStatuses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TargetNotifiers",
                table: "Notifications",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkProcessSetId",
                table: "WorkOrderInfos");

            migrationBuilder.AlterColumn<long>(
                name: "WorkOrderBomId",
                table: "WorkOrderInfos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryTime",
                table: "WorkOrderInfos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TargetNotifiers",
                table: "UserNotifications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "MaterialInfoId",
                table: "OrderMaterialProduceStatuses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "MaterialId",
                table: "OrderMaterialProduceStatuses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "TargetNotifiers",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderMaterialProduceStatuses_MaterialInfoId",
                table: "OrderMaterialProduceStatuses",
                column: "MaterialInfoId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_OrderMaterialProduceStatuses_MaterialInfos_MaterialInfoId",
            //    table: "OrderMaterialProduceStatuses",
            //    column: "MaterialInfoId",
            //    principalTable: "MaterialInfos",
            //    principalColumn: "Id");
        }
    }
}
