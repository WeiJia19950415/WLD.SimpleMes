using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.ReportDb
{
    public partial class Add_WeekOnePassRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DDWeekOnePassRateReports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<long>(type: "bigint", nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ProductLineId = table.Column<long>(type: "bigint", nullable: false),
                    ProductLineName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TotalTestCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PassCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RepairedTestCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NormalTestCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NoramlPassCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RepairedPassDDCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BelongWeek = table.Column<int>(type: "int", nullable: false),
                    BelongYear = table.Column<int>(type: "int", nullable: false),
                    BelongMonth = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DDWeekOnePassRateReports", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DDWeekOnePassRateReports_DataDate",
                table: "DDWeekOnePassRateReports",
                column: "DataDate");

            migrationBuilder.CreateIndex(
                name: "IX_DDWeekOnePassRateReports_MaterialId",
                table: "DDWeekOnePassRateReports",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_DDWeekOnePassRateReports_MaterialNumber",
                table: "DDWeekOnePassRateReports",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DDWeekOnePassRateReports_ProductLineId",
                table: "DDWeekOnePassRateReports",
                column: "ProductLineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DDWeekOnePassRateReports");
        }
    }
}
