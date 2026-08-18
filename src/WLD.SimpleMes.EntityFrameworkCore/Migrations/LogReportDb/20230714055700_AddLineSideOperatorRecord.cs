using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class AddLineSideOperatorRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LineSideMaterialOperatorRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineSideMaterialInfoId = table.Column<long>(type: "bigint", nullable: false),
                    OperatorWorkShopId = table.Column<long>(type: "bigint", nullable: false),
                    WorkOrderNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OpertaorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OpertaorId = table.Column<long>(type: "bigint", nullable: false),
                    OperatorTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperatorStockTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperatorCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockOperatoerType = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineSideMaterialOperatorRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineSideMaterialOperatorRecords_LineSideMaterialInfoId",
                table: "LineSideMaterialOperatorRecords",
                column: "LineSideMaterialInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_LineSideMaterialOperatorRecords_OperatorWorkShopId",
                table: "LineSideMaterialOperatorRecords",
                column: "OperatorWorkShopId");

            migrationBuilder.CreateIndex(
                name: "IX_LineSideMaterialOperatorRecords_OpertaorId",
                table: "LineSideMaterialOperatorRecords",
                column: "OpertaorId");

            migrationBuilder.CreateIndex(
                name: "IX_LineSideMaterialOperatorRecords_WorkOrderNumber",
                table: "LineSideMaterialOperatorRecords",
                column: "WorkOrderNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineSideMaterialOperatorRecords");
        }
    }
}
