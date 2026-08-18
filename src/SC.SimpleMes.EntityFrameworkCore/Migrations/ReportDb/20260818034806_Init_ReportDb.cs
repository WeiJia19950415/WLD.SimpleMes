using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.ReportDb
{
    public partial class Init_ReportDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrgProductProcessWorkLoadReport",
                table: "OrgProductProcessWorkLoadReport");

            migrationBuilder.RenameTable(
                name: "OrgProductProcessWorkLoadReport",
                newName: "OrgProductProcessWorkLoadReports");

            migrationBuilder.RenameIndex(
                name: "IX_OrgProductProcessWorkLoadReport_WorkStationId",
                table: "OrgProductProcessWorkLoadReports",
                newName: "IX_OrgProductProcessWorkLoadReports_WorkStationId");

            migrationBuilder.RenameIndex(
                name: "IX_OrgProductProcessWorkLoadReport_WorkProcessId",
                table: "OrgProductProcessWorkLoadReports",
                newName: "IX_OrgProductProcessWorkLoadReports_WorkProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_OrgProductProcessWorkLoadReport_ProductLineId",
                table: "OrgProductProcessWorkLoadReports",
                newName: "IX_OrgProductProcessWorkLoadReports_ProductLineId");

            migrationBuilder.RenameIndex(
                name: "IX_OrgProductProcessWorkLoadReport_MaterialNumber",
                table: "OrgProductProcessWorkLoadReports",
                newName: "IX_OrgProductProcessWorkLoadReports_MaterialNumber");

            migrationBuilder.RenameIndex(
                name: "IX_OrgProductProcessWorkLoadReport_MaterialInfoId",
                table: "OrgProductProcessWorkLoadReports",
                newName: "IX_OrgProductProcessWorkLoadReports_MaterialInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrgProductProcessWorkLoadReports",
                table: "OrgProductProcessWorkLoadReports",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrgProductProcessWorkLoadReports",
                table: "OrgProductProcessWorkLoadReports");

            migrationBuilder.RenameTable(
                name: "OrgProductProcessWorkLoadReports",
                newName: "OrgProductProcessWorkLoadReport");

            migrationBuilder.RenameIndex(
                name: "IX_OrgProductProcessWorkLoadReports_WorkStationId",
                table: "OrgProductProcessWorkLoadReport",
                newName: "IX_OrgProductProcessWorkLoadReport_WorkStationId");

            migrationBuilder.RenameIndex(
                name: "IX_OrgProductProcessWorkLoadReports_WorkProcessId",
                table: "OrgProductProcessWorkLoadReport",
                newName: "IX_OrgProductProcessWorkLoadReport_WorkProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_OrgProductProcessWorkLoadReports_ProductLineId",
                table: "OrgProductProcessWorkLoadReport",
                newName: "IX_OrgProductProcessWorkLoadReport_ProductLineId");

            migrationBuilder.RenameIndex(
                name: "IX_OrgProductProcessWorkLoadReports_MaterialNumber",
                table: "OrgProductProcessWorkLoadReport",
                newName: "IX_OrgProductProcessWorkLoadReport_MaterialNumber");

            migrationBuilder.RenameIndex(
                name: "IX_OrgProductProcessWorkLoadReports_MaterialInfoId",
                table: "OrgProductProcessWorkLoadReport",
                newName: "IX_OrgProductProcessWorkLoadReport_MaterialInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrgProductProcessWorkLoadReport",
                table: "OrgProductProcessWorkLoadReport",
                column: "Id");
        }
    }
}
