using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class ProblemRecord_AddIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BelongProductLineId",
                table: "ProblemRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "BelongWorkStaionId",
                table: "ProblemRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CloseTime",
                table: "ProblemRecords",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEffect",
                table: "ProblemRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BelongProductLineId",
                table: "ProblemRecords");

            migrationBuilder.DropColumn(
                name: "BelongWorkStaionId",
                table: "ProblemRecords");

            migrationBuilder.DropColumn(
                name: "CloseTime",
                table: "ProblemRecords");

            migrationBuilder.DropColumn(
                name: "IsEffect",
                table: "ProblemRecords");
        }
    }
}
