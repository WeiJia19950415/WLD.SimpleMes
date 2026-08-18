using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.ReportDb
{
    public partial class A_WorkProcessLoad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrgProductProcessWorkLoadReport",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductLineId = table.Column<long>(type: "bigint", nullable: false),
                    ProductLineName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkStationId = table.Column<long>(type: "bigint", nullable: false),
                    WorkStationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    WorkProcessId = table.Column<long>(type: "bigint", nullable: false),
                    ProcessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialInfoId = table.Column<long>(type: "bigint", nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ReceivedCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinishedProductCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinishedRepairProductCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StaticDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgProductProcessWorkLoadReport", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrgProductProcessWorkLoadReport_MaterialInfoId",
                table: "OrgProductProcessWorkLoadReport",
                column: "MaterialInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgProductProcessWorkLoadReport_MaterialNumber",
                table: "OrgProductProcessWorkLoadReport",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_OrgProductProcessWorkLoadReport_ProductLineId",
                table: "OrgProductProcessWorkLoadReport",
                column: "ProductLineId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgProductProcessWorkLoadReport_WorkProcessId",
                table: "OrgProductProcessWorkLoadReport",
                column: "WorkProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgProductProcessWorkLoadReport_WorkStationId",
                table: "OrgProductProcessWorkLoadReport",
                column: "WorkStationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrgProductProcessWorkLoadReport");
        }
    }
}
