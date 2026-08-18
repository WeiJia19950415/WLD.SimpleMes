using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.LogReportDb
{
    public partial class AddWorkProcessMaterialRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkProcessOperatorRecords_WorkOrderId",
                table: "WorkProcessOperatorRecords");

            migrationBuilder.DropColumn(
                name: "WorkOrderId",
                table: "WorkProcessOperatorRecords");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WorkOrderId",
                table: "WorkProcessOperatorRecords",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessOperatorRecords_WorkOrderId",
                table: "WorkProcessOperatorRecords",
                column: "WorkOrderId");
        }
    }
}
