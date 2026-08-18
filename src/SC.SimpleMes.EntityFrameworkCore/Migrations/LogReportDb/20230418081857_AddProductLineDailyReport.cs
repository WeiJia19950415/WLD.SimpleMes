using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.LogReportDb
{
    public partial class AddProductLineDailyReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductLineCapacityDailyReportRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<long>(type: "bigint", nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProductLineId = table.Column<long>(type: "bigint", nullable: false),
                    ProductLineName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InputCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinishedCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StaticDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLineCapacityDailyReportRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductLineCapacityDailyReportRecords_MaterialId",
                table: "ProductLineCapacityDailyReportRecords",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLineCapacityDailyReportRecords_MaterialNumber",
                table: "ProductLineCapacityDailyReportRecords",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLineCapacityDailyReportRecords_ProductLineId",
                table: "ProductLineCapacityDailyReportRecords",
                column: "ProductLineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLineCapacityDailyReportRecords_StaticDate",
                table: "ProductLineCapacityDailyReportRecords",
                column: "StaticDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductLineCapacityDailyReportRecords");
        }
    }
}
