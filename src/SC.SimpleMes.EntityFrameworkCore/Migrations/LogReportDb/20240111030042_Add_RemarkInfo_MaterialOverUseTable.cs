using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.LogReportDb
{
    public partial class Add_RemarkInfo_MaterialOverUseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "WarningOverUsedERPInStockInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemarkDateTime",
                table: "WarningOverUsedERPInStockInfos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RemarkUserId",
                table: "WarningOverUsedERPInStockInfos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemarkUserName",
                table: "WarningOverUsedERPInStockInfos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "WarningOverUsedERPInStockInfos");

            migrationBuilder.DropColumn(
                name: "RemarkDateTime",
                table: "WarningOverUsedERPInStockInfos");

            migrationBuilder.DropColumn(
                name: "RemarkUserId",
                table: "WarningOverUsedERPInStockInfos");

            migrationBuilder.DropColumn(
                name: "RemarkUserName",
                table: "WarningOverUsedERPInStockInfos");
        }
    }
}
