using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.ReportDb
{
    public partial class UpdatePreparieDayReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WorkStationId",
                table: "PrepaireWorkProcessDayReports",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "WorkStationName",
                table: "PrepaireWorkProcessDayReports",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PrepaireWorkProcessDayReports_WorkStationId",
                table: "PrepaireWorkProcessDayReports",
                column: "WorkStationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PrepaireWorkProcessDayReports_WorkStationId",
                table: "PrepaireWorkProcessDayReports");

            migrationBuilder.DropColumn(
                name: "WorkStationId",
                table: "PrepaireWorkProcessDayReports");

            migrationBuilder.DropColumn(
                name: "WorkStationName",
                table: "PrepaireWorkProcessDayReports");
        }
    }
}
