using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class U_DcicardContorl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CheckWarpCount",
                table: "ProblemRecords",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ProblemWarpCount",
                table: "ProblemRecords",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarpUnitName",
                table: "ProblemRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiccardWarpCount",
                table: "MaterialDiscardRecord",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "WrapUnitName",
                table: "MaterialDiscardRecord",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialStatu",
                table: "MaterialBatchNumbers",
                type: "int",
                nullable: true,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "MaterialStatu",
                table: "ERPInStockInfos",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "ERPInStockRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OperatorId = table.Column<long>(type: "bigint", nullable: false),
                    Operator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperateDesp = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    OperateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERPInStockRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ERPInStockRecords_BatchNo",
                table: "ERPInStockRecords",
                column: "BatchNo");

            migrationBuilder.CreateIndex(
                name: "IX_ERPInStockRecords_MaterialNumber",
                table: "ERPInStockRecords",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ERPInStockRecords_OperateTime",
                table: "ERPInStockRecords",
                column: "OperateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ERPInStockRecords");

            migrationBuilder.DropColumn(
                name: "MaterialStatu",
                table: "WarningOverUsedERPInStockInfos");

            migrationBuilder.DropColumn(
                name: "CheckWarpCount",
                table: "ProblemRecords");

            migrationBuilder.DropColumn(
                name: "ProblemWarpCount",
                table: "ProblemRecords");

            migrationBuilder.DropColumn(
                name: "WarpUnitName",
                table: "ProblemRecords");

            migrationBuilder.DropColumn(
                name: "DiccardWarpCount",
                table: "MaterialDiscardRecord");

            migrationBuilder.DropColumn(
                name: "WrapUnitName",
                table: "MaterialDiscardRecord");

            migrationBuilder.DropColumn(
                name: "MaterialStatu",
                table: "MaterialBatchNumbers");

            migrationBuilder.DropColumn(
                name: "MaterialStatu",
                table: "ERPInStockInfos");
        }
    }
}
