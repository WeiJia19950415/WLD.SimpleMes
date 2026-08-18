using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.LogReportDb
{
    public partial class ReName_UpdateWorkProcessCapacityDailyReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StationCapacityDailyReportRecords",
                table: "StationCapacityDailyReportRecords");

            migrationBuilder.RenameTable(
                name: "StationCapacityDailyReportRecords",
                newName: "WorkProcessCapacityDailyReportRecord");

            migrationBuilder.RenameIndex(
                name: "IX_StationCapacityDailyReportRecords_WorkStationId",
                table: "WorkProcessCapacityDailyReportRecord",
                newName: "IX_WorkProcessCapacityDailyReportRecord_WorkStationId");

            migrationBuilder.RenameIndex(
                name: "IX_StationCapacityDailyReportRecords_StaticDate",
                table: "WorkProcessCapacityDailyReportRecord",
                newName: "IX_WorkProcessCapacityDailyReportRecord_StaticDate");

            migrationBuilder.RenameIndex(
                name: "IX_StationCapacityDailyReportRecords_ProductLineId",
                table: "WorkProcessCapacityDailyReportRecord",
                newName: "IX_WorkProcessCapacityDailyReportRecord_ProductLineId");

            migrationBuilder.RenameIndex(
                name: "IX_StationCapacityDailyReportRecords_MaterialNumber",
                table: "WorkProcessCapacityDailyReportRecord",
                newName: "IX_WorkProcessCapacityDailyReportRecord_MaterialNumber");

            migrationBuilder.RenameIndex(
                name: "IX_StationCapacityDailyReportRecords_MaterialId",
                table: "WorkProcessCapacityDailyReportRecord",
                newName: "IX_WorkProcessCapacityDailyReportRecord_MaterialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkProcessCapacityDailyReportRecord",
                table: "WorkProcessCapacityDailyReportRecord",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkProcessCapacityDailyReportRecord",
                table: "WorkProcessCapacityDailyReportRecord");

            migrationBuilder.RenameTable(
                name: "WorkProcessCapacityDailyReportRecord",
                newName: "StationCapacityDailyReportRecords");

            migrationBuilder.RenameIndex(
                name: "IX_WorkProcessCapacityDailyReportRecord_WorkStationId",
                table: "StationCapacityDailyReportRecords",
                newName: "IX_StationCapacityDailyReportRecords_WorkStationId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkProcessCapacityDailyReportRecord_StaticDate",
                table: "StationCapacityDailyReportRecords",
                newName: "IX_StationCapacityDailyReportRecords_StaticDate");

            migrationBuilder.RenameIndex(
                name: "IX_WorkProcessCapacityDailyReportRecord_ProductLineId",
                table: "StationCapacityDailyReportRecords",
                newName: "IX_StationCapacityDailyReportRecords_ProductLineId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkProcessCapacityDailyReportRecord_MaterialNumber",
                table: "StationCapacityDailyReportRecords",
                newName: "IX_StationCapacityDailyReportRecords_MaterialNumber");

            migrationBuilder.RenameIndex(
                name: "IX_WorkProcessCapacityDailyReportRecord_MaterialId",
                table: "StationCapacityDailyReportRecords",
                newName: "IX_StationCapacityDailyReportRecords_MaterialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StationCapacityDailyReportRecords",
                table: "StationCapacityDailyReportRecords",
                column: "Id");
        }
    }
}
