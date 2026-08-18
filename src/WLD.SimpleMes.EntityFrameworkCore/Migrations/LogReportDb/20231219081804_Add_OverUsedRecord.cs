using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class Add_OverUsedRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WarningOverUsedERPInStockInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstWarningTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastWarningTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualUseAmount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarningOverUsedERPInStockInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarningOverUsedWorkOrderRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkOrderNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FirstWarningTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastWarningTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarningOverUsedWorkOrderRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarningOverUsedERPInStockInfos_BatchNo",
                table: "WarningOverUsedERPInStockInfos",
                column: "BatchNo");

            migrationBuilder.CreateIndex(
                name: "IX_WarningOverUsedERPInStockInfos_FirstWarningTime",
                table: "WarningOverUsedERPInStockInfos",
                column: "FirstWarningTime");

            migrationBuilder.CreateIndex(
                name: "IX_WarningOverUsedWorkOrderRecords_FirstWarningTime",
                table: "WarningOverUsedWorkOrderRecords",
                column: "FirstWarningTime");

            migrationBuilder.CreateIndex(
                name: "IX_WarningOverUsedWorkOrderRecords_WorkOrderNumber",
                table: "WarningOverUsedWorkOrderRecords",
                column: "WorkOrderNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarningOverUsedERPInStockInfos");

            migrationBuilder.DropTable(
                name: "WarningOverUsedWorkOrderRecords");
        }
    }
}
