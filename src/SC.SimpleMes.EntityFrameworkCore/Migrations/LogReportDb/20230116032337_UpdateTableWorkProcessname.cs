using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.LogReportDb
{
    public partial class UpdateTableWorkProcessname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkProcessName",
                table: "WorkProcessMaterialRecordHistory");

            migrationBuilder.AddColumn<bool>(
                name: "IsNormalFinish",
                table: "WorkProcessOperatorRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WorkProcessName",
                table: "WorkProcessOperatorRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkProcessName",
                table: "FormInfoRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNormalFinish",
                table: "WorkProcessOperatorRecords");

            migrationBuilder.DropColumn(
                name: "WorkProcessName",
                table: "WorkProcessOperatorRecords");

            migrationBuilder.DropColumn(
                name: "WorkProcessName",
                table: "FormInfoRecords");

            migrationBuilder.AddColumn<string>(
                name: "WorkProcessName",
                table: "WorkProcessMaterialRecordHistory",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
