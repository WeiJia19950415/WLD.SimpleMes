using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.LogReportDb
{
    public partial class U_PoblemRecord_AuditTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AuditTime",
                table: "ProblemRecords",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AuditorId",
                table: "ProblemRecords",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuditorName",
                table: "ProblemRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuditTime",
                table: "ProblemRecords");

            migrationBuilder.DropColumn(
                name: "AuditorId",
                table: "ProblemRecords");

            migrationBuilder.DropColumn(
                name: "AuditorName",
                table: "ProblemRecords");
        }
    }
}
