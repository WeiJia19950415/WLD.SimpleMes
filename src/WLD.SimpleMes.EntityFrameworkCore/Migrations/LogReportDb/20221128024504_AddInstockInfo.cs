using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class AddInstockInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromErpBatchNumber",
                table: "MaterialBatchNumbers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BatchNumberPrintRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BatchNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PrintCounts = table.Column<int>(type: "int", nullable: false),
                    PrintTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrintMachine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OperatorId = table.Column<long>(type: "bigint", nullable: false),
                    OperatorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchNumberPrintRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ERPInStockInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceType = table.Column<int>(type: "int", nullable: false),
                    WarehousingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WarehousingNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UnitName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BatchNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ReceiptQuantity = table.Column<decimal>(type: "decimal(20,3)", precision: 20, scale: 3, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERPInStockInfos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BatchNumberPrintRecords_BatchNumber",
                table: "BatchNumberPrintRecords",
                column: "BatchNumber");

            migrationBuilder.CreateIndex(
                name: "IX_BatchNumberPrintRecords_MaterialNumber",
                table: "BatchNumberPrintRecords",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_BatchNumberPrintRecords_OperatorId",
                table: "BatchNumberPrintRecords",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchNumberPrintRecords_PrintTime",
                table: "BatchNumberPrintRecords",
                column: "PrintTime");

            migrationBuilder.CreateIndex(
                name: "IX_ERPInStockInfos_BatchNo",
                table: "ERPInStockInfos",
                column: "BatchNo");

            migrationBuilder.CreateIndex(
                name: "IX_ERPInStockInfos_CreateTime",
                table: "ERPInStockInfos",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ERPInStockInfos_MaterialNumber",
                table: "ERPInStockInfos",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ERPInStockInfos_SourceType",
                table: "ERPInStockInfos",
                column: "SourceType");

            migrationBuilder.CreateIndex(
                name: "IX_ERPInStockInfos_WarehousingNumber",
                table: "ERPInStockInfos",
                column: "WarehousingNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ERPInStockInfos_WarehousingTime",
                table: "ERPInStockInfos",
                column: "WarehousingTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatchNumberPrintRecords");

            migrationBuilder.DropTable(
                name: "ERPInStockInfos");

            migrationBuilder.DropColumn(
                name: "FromErpBatchNumber",
                table: "MaterialBatchNumbers");
        }
    }
}
