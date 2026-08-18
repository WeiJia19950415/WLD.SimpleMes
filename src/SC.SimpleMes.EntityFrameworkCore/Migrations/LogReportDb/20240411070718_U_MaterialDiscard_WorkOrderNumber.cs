using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.LogReportDb
{
    public partial class U_MaterialDiscard_WorkOrderNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkOrderNumber",
                table: "MaterialDiscardRecord",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkOrderNumber",
                table: "MaterialDiscardRecord");
        }
    }
}
