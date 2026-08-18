using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class Add_ProblemDailyReportTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    ProductLineName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WorkStationId = table.Column<long>(type: "bigint", nullable: false),
                    WorkStationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkProcessId = table.Column<long>(type: "bigint", nullable: false),
                    WorkProcessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemCount = table.Column<int>(type: "int", nullable: false),
                    StaticDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProcessProblemDailyReportRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessProblemDailyReportRecords_MaterialId",
                table: "WorkProcessProblemDailyReportRecords",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessProblemDailyReportRecords_MaterialNumber",
                table: "WorkProcessProblemDailyReportRecords",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessProblemDailyReportRecords_ProductLineId",
                table: "WorkProcessProblemDailyReportRecords",
                column: "ProductLineId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessProblemDailyReportRecords_StaticDate",
                table: "WorkProcessProblemDailyReportRecords",
                column: "StaticDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkProcessProblemDailyReportRecords");
        }
    }
}
