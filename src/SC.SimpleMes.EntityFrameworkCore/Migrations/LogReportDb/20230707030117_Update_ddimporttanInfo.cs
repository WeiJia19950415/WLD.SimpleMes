using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.LogReportDb
{
    public partial class Update_ddimporttanInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AuditeDate",
                table: "DDImportantInfos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Auditor",
                table: "DDImportantInfos",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AuditorId",
                table: "DDImportantInfos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AvgChargeOCV",
                table: "DDImportantInfos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AvgChargeTime",
                table: "DDImportantInfos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AvgDischargeOCV",
                table: "DDImportantInfos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AvgDischargeTime",
                table: "DDImportantInfos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ChargeAvgInternalResistance",
                table: "DDImportantInfos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckDate",
                table: "DDImportantInfos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Checkor",
                table: "DDImportantInfos",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CheckorId",
                table: "DDImportantInfos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "DischargeAvgInternalResistance",
                table: "DDImportantInfos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DryWeight",
                table: "DDImportantInfos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Humidity",
                table: "DDImportantInfos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsAudited",
                table: "DDImportantInfos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "DDImportantInfos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoopCount",
                table: "DDImportantInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "NegativeAvgFlowRate",
                table: "DDImportantInfos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NegativeCanAvgTempeature",
                table: "DDImportantInfos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PostiveAvgFlowRate",
                table: "DDImportantInfos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PostiveCanAvgTempeature",
                table: "DDImportantInfos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "DDImportantInfos",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SendDate",
                table: "DDImportantInfos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "WetWeight",
                table: "DDImportantInfos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuditeDate",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "Auditor",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "AuditorId",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "AvgChargeOCV",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "AvgChargeTime",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "AvgDischargeOCV",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "AvgDischargeTime",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "ChargeAvgInternalResistance",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "CheckDate",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "Checkor",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "CheckorId",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "DischargeAvgInternalResistance",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "DryWeight",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "Humidity",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "IsAudited",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "LoopCount",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "NegativeAvgFlowRate",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "NegativeCanAvgTempeature",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "PostiveAvgFlowRate",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "PostiveCanAvgTempeature",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "SendDate",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "WetWeight",
                table: "DDImportantInfos");
        }
    }
}
