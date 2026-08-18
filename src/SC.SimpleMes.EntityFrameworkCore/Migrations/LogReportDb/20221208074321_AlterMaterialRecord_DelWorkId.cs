using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.LogReportDb
{
    public partial class AlterMaterialRecord_DelWorkId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkProcessMaterialRecords_WorkOrderId",
                table: "WorkProcessMaterialRecords");

            migrationBuilder.DropColumn(
                name: "WorkOrderId",
                table: "WorkProcessMaterialRecords");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WorkOrderId",
                table: "WorkProcessMaterialRecords",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessMaterialRecords_WorkOrderId",
                table: "WorkProcessMaterialRecords",
                column: "WorkOrderId");
        }
    }
}
