using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class UpdateOperatorRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostTime",
                table: "WorkProcessOperatorRecords");

            migrationBuilder.AddColumn<long>(
                name: "CostTimeSeconds",
                table: "WorkProcessOperatorRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostTimeSeconds",
                table: "WorkProcessOperatorRecords");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "CostTime",
                table: "WorkProcessOperatorRecords",
                type: "time",
                nullable: true);
        }
    }
}
