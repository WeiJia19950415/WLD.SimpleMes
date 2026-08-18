using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class Add_SupplierInfos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BatchNo",
                table: "WorkProcessMaterialRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Supplier",
                table: "WorkProcessMaterialRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WarehousingTime",
                table: "WorkProcessMaterialRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BatchNo",
                table: "WorkProcessMaterialRecordHistory",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Supplier",
                table: "WorkProcessMaterialRecordHistory",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WarehousingTime",
                table: "WorkProcessMaterialRecordHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ExtensionData",
                table: "DDImportantInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PileThickness",
                table: "DDImportantInfos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchNo",
                table: "WorkProcessMaterialRecords");

            migrationBuilder.DropColumn(
                name: "Supplier",
                table: "WorkProcessMaterialRecords");

            migrationBuilder.DropColumn(
                name: "WarehousingTime",
                table: "WorkProcessMaterialRecords");

            migrationBuilder.DropColumn(
                name: "BatchNo",
                table: "WorkProcessMaterialRecordHistory");

            migrationBuilder.DropColumn(
                name: "Supplier",
                table: "WorkProcessMaterialRecordHistory");

            migrationBuilder.DropColumn(
                name: "WarehousingTime",
                table: "WorkProcessMaterialRecordHistory");

            migrationBuilder.DropColumn(
                name: "ExtensionData",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "PileThickness",
                table: "DDImportantInfos");
        }
    }
}
