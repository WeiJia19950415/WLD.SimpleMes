using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class Add_UpdateWorkProcessCapacityDailyReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WorkProcessId",
                table: "StationCapacityDailyReportRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "WorkProcessName",
                table: "StationCapacityDailyReportRecords",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkProcessId",
                table: "StationCapacityDailyReportRecords");

            migrationBuilder.DropColumn(
                name: "WorkProcessName",
                table: "StationCapacityDailyReportRecords");
        }
    }
}
