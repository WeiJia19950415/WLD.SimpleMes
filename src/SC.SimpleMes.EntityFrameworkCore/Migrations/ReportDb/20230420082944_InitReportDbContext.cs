using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.ReportDb
{
    public partial class InitReportDbContext : Migration
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

            migrationBuilder.CreateTable(
                name: "WorkProcessCapacityDailyReportRecord",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<long>(type: "bigint", nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProductLineId = table.Column<long>(type: "bigint", nullable: false),
                    ProductLineName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WorkStationId = table.Column<long>(type: "bigint", nullable: false),
                    WorkStationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WorkProcessId = table.Column<long>(type: "bigint", nullable: false),
                    WorkProcessName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InputCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinishedCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StaticDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProcessCapacityDailyReportRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkProcessProblemDailyReportRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<long>(type: "bigint", nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProductLineId = table.Column<long>(type: "bigint", nullable: false),
                    ProductLineName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    WorkStationId = table.Column<long>(type: "bigint", nullable: false),
                    WorkStationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    WorkProcessId = table.Column<long>(type: "bigint", nullable: false),
                    WorkProcessName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProblemDefineId = table.Column<long>(type: "bigint", nullable: false),
                    ProbleName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    QualityProblemNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataCount = table.Column<int>(type: "int", nullable: false),
                    StaticDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProcessProblemDailyReportRecords", x => x.Id);
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

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessCapacityDailyReportRecord_MaterialId",
                table: "WorkProcessCapacityDailyReportRecord",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessCapacityDailyReportRecord_MaterialNumber",
                table: "WorkProcessCapacityDailyReportRecord",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessCapacityDailyReportRecord_ProductLineId",
                table: "WorkProcessCapacityDailyReportRecord",
                column: "ProductLineId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessCapacityDailyReportRecord_StaticDate",
                table: "WorkProcessCapacityDailyReportRecord",
                column: "StaticDate");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessCapacityDailyReportRecord_WorkStationId",
                table: "WorkProcessCapacityDailyReportRecord",
                column: "WorkStationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessProblemDailyReportRecords_MaterialId",
                table: "WorkProcessProblemDailyReportRecords",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessProblemDailyReportRecords_MaterialNumber",
                table: "WorkProcessProblemDailyReportRecords",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessProblemDailyReportRecords_ProblemDefineId",
                table: "WorkProcessProblemDailyReportRecords",
                column: "ProblemDefineId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessProblemDailyReportRecords_ProductLineId",
                table: "WorkProcessProblemDailyReportRecords",
                column: "ProductLineId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessProblemDailyReportRecords_QualityProblemNumber",
                table: "WorkProcessProblemDailyReportRecords",
                column: "QualityProblemNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessProblemDailyReportRecords_StaticDate",
                table: "WorkProcessProblemDailyReportRecords",
                column: "StaticDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductLineCapacityDailyReportRecords");

            migrationBuilder.DropTable(
                name: "WorkProcessCapacityDailyReportRecord");

            migrationBuilder.DropTable(
                name: "WorkProcessProblemDailyReportRecords");
        }
    }
}
