using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.ReportDb
{
    public partial class addOnePassRateReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrepaireWorkProcessDayReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<long>(type: "bigint", nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProductLineId = table.Column<long>(type: "bigint", nullable: false),
                    ProductLineName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FinishedCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CutMaterialUnitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BomUniteCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BomUnitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaticDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepaireWorkProcessDayReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductLineOnePassRateReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<long>(type: "bigint", nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProductLineId = table.Column<long>(type: "bigint", nullable: false),
                    ProductLineName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExpectionCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinishedCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OnePassReate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StaticDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLineOnePassRateReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkProcessOnePassRateReports",
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
                    ExpectionCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinishedCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OnePassReate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StaticDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProcessOnePassRateReports", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrepaireWorkProcessDayReports_MaterialId",
                table: "PrepaireWorkProcessDayReports",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_PrepaireWorkProcessDayReports_MaterialNumber",
                table: "PrepaireWorkProcessDayReports",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_PrepaireWorkProcessDayReports_ProductLineId",
                table: "PrepaireWorkProcessDayReports",
                column: "ProductLineId");

            migrationBuilder.CreateIndex(
                name: "IX_PrepaireWorkProcessDayReports_StaticDate",
                table: "PrepaireWorkProcessDayReports",
                column: "StaticDate");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLineOnePassRateReports_MaterialId",
                table: "ProductLineOnePassRateReports",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLineOnePassRateReports_MaterialNumber",
                table: "ProductLineOnePassRateReports",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLineOnePassRateReports_ProductLineId",
                table: "ProductLineOnePassRateReports",
                column: "ProductLineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLineOnePassRateReports_StaticDate",
                table: "ProductLineOnePassRateReports",
                column: "StaticDate");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessOnePassRateReports_MaterialId",
                table: "WorkProcessOnePassRateReports",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessOnePassRateReports_MaterialNumber",
                table: "WorkProcessOnePassRateReports",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessOnePassRateReports_ProductLineId",
                table: "WorkProcessOnePassRateReports",
                column: "ProductLineId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessOnePassRateReports_StaticDate",
                table: "WorkProcessOnePassRateReports",
                column: "StaticDate");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessOnePassRateReports_WorkStationId",
                table: "WorkProcessOnePassRateReports",
                column: "WorkStationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrepaireWorkProcessDayReports");

            migrationBuilder.DropTable(
                name: "ProductLineOnePassRateReports");

            migrationBuilder.DropTable(
                name: "WorkProcessOnePassRateReports");
        }
    }
}
