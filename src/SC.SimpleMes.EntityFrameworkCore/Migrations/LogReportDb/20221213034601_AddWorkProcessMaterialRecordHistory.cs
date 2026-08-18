using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.LogReportDb
{
    public partial class AddWorkProcessMaterialRecordHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkProcessMaterialRecordHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WrokShopId = table.Column<long>(type: "bigint", nullable: false),
                    ProductLineId = table.Column<long>(type: "bigint", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    WorkProcessId = table.Column<long>(type: "bigint", nullable: false),
                    WorkProcessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkStationId = table.Column<long>(type: "bigint", nullable: false),
                    WorkStationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InputMaterilId = table.Column<long>(type: "bigint", nullable: false),
                    InputMaterialBatchNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    InputMaterialCount = table.Column<decimal>(type: "decimal(20,3)", precision: 20, scale: 3, nullable: true),
                    OutRangeCount = table.Column<decimal>(type: "decimal(20,3)", precision: 20, scale: 3, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProcessMaterialRecordHistory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessMaterialRecordHistory_InputMaterialBatchNumber",
                table: "WorkProcessMaterialRecordHistory",
                column: "InputMaterialBatchNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessMaterialRecordHistory_OrderNumber",
                table: "WorkProcessMaterialRecordHistory",
                column: "OrderNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessMaterialRecordHistory_ProductLineId",
                table: "WorkProcessMaterialRecordHistory",
                column: "ProductLineId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessMaterialRecordHistory_WorkProcessId",
                table: "WorkProcessMaterialRecordHistory",
                column: "WorkProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessMaterialRecordHistory_WorkStationId",
                table: "WorkProcessMaterialRecordHistory",
                column: "WorkStationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkProcessMaterialRecordHistory");
        }
    }
}
